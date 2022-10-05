using AutoMapper;
using Gateways.Core.Entities;
using Gateways.Database;
using Gateways.Services.Common.Exceptions;
using Gateways.Services.Common.Sieve.Pagination;
using Humanizer;
using Microsoft.Extensions.Logging;
using Sieve.Models;
using Sieve.Services;
using Gateways.Services.Devices.Models;
using Microsoft.EntityFrameworkCore;
using Gateways.Services.Common.Sieve.Extensions;
using FluentValidation;
using Gateways.Services.Common.Validations;

namespace Gateways.Services.Devices
{
    /// <summary>
    /// Devices service
    /// </summary>
    public class DevicesService : IDevicesService
    {
        private readonly GatewaysContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<DevicesService> _logger;
        private readonly ISieveProcessor _sieveProcessor;
        private readonly IValidator<Device> _validator;

        /// <summary>
        /// Main constructor
        /// </summary>
        public DevicesService(
            GatewaysContext context,
            IMapper mapper,
            ILogger<DevicesService> logger,
            ISieveProcessor sieveProcessor,
            IValidator<Device> validator)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _sieveProcessor = sieveProcessor;
            _validator = validator;
        }

        ///<inheritdoc/>
        public async Task<PagedResult<DeviceDto>> GetAll(SieveModel query)
        {
            try
            {
                var devices = _context.Devices!.AsNoTracking().Include(x => x.Gateway);
                return await _sieveProcessor.GetPagedAsync<Device, DeviceDto>(devices, sieveModel: query, configurationProvider: _mapper.ConfigurationProvider);
            }
            catch (Exception ex)
            {
                var msg = $"Service error while getting {nameof(Device).ToString().Humanize(LetterCasing.Title)} list. See the exception for details.";
                _logger.LogError(ex, msg);
                throw new ServiceException(ex.Message);
            }
        }


        ///<inheritdoc/>
        public async Task<DeviceDto> GetById(int id, CancellationToken cancellation)
        {
            try
            {
                var device = await _context.Devices!.AsNoTracking().Include(x => x.Gateway).Where(x => x.Uid.Equals(id)).SingleOrDefaultAsync(cancellation);
                if (device is null)
                {
                    throw new NotFoundException($"{nameof(Device).ToString().Humanize(LetterCasing.Title)} does not exist.");
                }
                _logger.LogInformation($"Get {typeof(Device).ToString().Humanize(LetterCasing.Title)}. Id: {id}");
                return _mapper.Map<DeviceDto>(device);
            }
            catch (NotFoundException ex)
            {
                var msg = $"Not found error while getting {nameof(Device).ToString().Humanize(LetterCasing.Title)}. See the exception for details.";
                _logger.LogError(ex, msg);
                throw ex;
            }
            catch (Exception ex)
            {
                var msg = $"Service error while getting {nameof(Device).ToString().Humanize(LetterCasing.Title)}. See the exception for details.";
                _logger.LogError(ex, msg);
                throw new ServiceException(ex.Message);
            }
        }

        ///<inheritdoc/>
        public async Task<DeviceDto> Create(AddDeviceDto dto, CancellationToken cancellation)
        {
            try
            {
                using (var context = new GatewaysContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var device = _mapper.Map<Device>(dto);

                            await ValidationUtils.ValidateAndThrow(ValidationUtils.CreateValidationContext(device, true, new string[] { ValidationAction.Add.ToString() }), _validator, cancellation);

                            var result = await _context.Devices!.AddAsync(device, cancellation);
                            await _context.SaveChangesAsync(cancellation);

                            var devices = await context.Devices!.Where(x => x.GatewayId.Equals(device.GatewayId)).ToListAsync(cancellation);
                            if (devices.Count < 10)
                            {
                                _logger.LogInformation($"{nameof(Device).ToString().Humanize(LetterCasing.Title)} added. Id: '{device!.Uid}'");
                                transaction.Commit();
                                return _mapper.Map<DeviceDto>(result.Entity);
                            } else
                            {
                                transaction.Rollback();
                                throw new ServiceException("The device can't be added because the gateways already have the maximum allowed devices.");
                            }
                        } catch (Exception ex)
                        {
                            transaction.Rollback();
                            var msg = $"Service error while adding {nameof(Device).ToString().Humanize(LetterCasing.Title)}. See the exception for details.";
                            _logger.LogError(ex, msg);
                            throw new ServiceException(ex.Message);
                        }
                    }
                }
            }
            catch (ValidationException ex)
            {
                var msg = $"Validation error while adding {nameof(Device).ToString().Humanize(LetterCasing.Title)}. See the exception for details.";
                _logger.LogError(ex, msg);
                throw new ServiceException(ex.Errors.ElementAt(0).ErrorMessage);
            }
            catch (Exception ex)
            {
                var msg = $"Service error while adding {nameof(Device).ToString().Humanize(LetterCasing.Title)}. See the exception for details.";
                _logger.LogError(ex, msg);
                throw new ServiceException(ex.Message);
            }
        }

        ///<inheritdoc/>
        public async Task Update(UpdateDeviceDto dto, CancellationToken cancellation)
        {
            try
            {
                var device = await _context.Devices!.Where(x => x.Uid.Equals(dto.Uid)).SingleOrDefaultAsync(cancellation);
                if (device is null)
                {
                    throw new NotFoundException($"{nameof(Device).ToString().Humanize(LetterCasing.Title)} does not exist.");
                }

                device = _mapper.Map(dto, device);

                await ValidationUtils.ValidateAndThrow(ValidationUtils.CreateValidationContext(device, true, new string[] { ValidationAction.Update.ToString() }), _validator, cancellation);

                await _context.SaveChangesAsync(cancellation);
                _logger.LogInformation($"{nameof(Device).ToString().Humanize(LetterCasing.Title)} updated. Id: '{device!.Uid}'");
            }
            catch (NotFoundException ex)
            {
                var msg = $"Not found error while updating {nameof(Device).ToString().Humanize(LetterCasing.Title)}. See the exception for details.";
                _logger.LogError(ex, msg);
                throw ex;
            }
            catch (ValidationException ex)
            {
                var msg = $"Validation error while updating {nameof(Device).ToString().Humanize(LetterCasing.Title)}. See the exception for details.";
                _logger.LogError(ex, msg);
                throw new ServiceException(ex.Errors.ElementAt(0).ErrorMessage);
            }
            catch (Exception ex)
            {
                var msg = $"Service error while updating {nameof(Device).ToString().Humanize(LetterCasing.Title)}. See the exception for details.";
                _logger.LogError(ex, msg);
                throw new ServiceException(ex.Message);
            }
        }

        ///<inheritdoc/>
        public async Task Delete(int id, CancellationToken cancellation)
        {
            try
            {
                var device = await _context.Devices!.Where(x => x.Uid.Equals(id)).SingleOrDefaultAsync(cancellation);
                if (device is null)
                {
                    throw new NotFoundException($"{nameof(Device).ToString().Humanize(LetterCasing.Title)} does not exist.");
                }

                _context.Devices!.Remove(device!);
                await _context.SaveChangesAsync(cancellation);
                _logger.LogInformation($"{nameof(Device).ToString().Humanize(LetterCasing.Title)} deleted. Id: {id}");
            }
            catch (NotFoundException ex)
            {
                var msg = $"Not found error while deleting {nameof(Device).ToString().Humanize(LetterCasing.Title)}. See the exception for details.";
                _logger.LogError(ex, msg);
                throw ex;
            }
            catch (Exception ex)
            {
                var msg = $"Service error while deleting {nameof(Device).ToString().Humanize(LetterCasing.Title)}. See the exception for details.";
                _logger.LogError(ex, msg);
                throw new ServiceException(ex.Message);
            }
        }
    }
}
