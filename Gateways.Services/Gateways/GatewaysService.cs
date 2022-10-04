using AutoMapper;
using FluentValidation;
using Gateways.Core.Entities;
using Gateways.Database;
using Gateways.Services.Common.Exceptions;
using Gateways.Services.Common.Sieve.Extensions;
using Gateways.Services.Common.Sieve.Pagination;
using Gateways.Services.Common.Validations;
using Gateways.Services.Gateways.Models;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sieve.Models;
using Sieve.Services;

namespace Gateways.Services.Gateways
{
    /// <summary>
    /// Gateways service
    /// </summary>
    public class GatewaysService : IGatewaysService
    {
        private readonly GatewaysContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GatewaysService> _logger;
        private readonly ISieveProcessor _sieveProcessor;
        private readonly IValidator<Gateway> _validator;

        /// <summary>
        /// Main constructor
        /// </summary>
        public GatewaysService(
            GatewaysContext context,
            IMapper mapper,
            ILogger<GatewaysService> logger,
            ISieveProcessor sieveProcessor,
            IValidator<Gateway> validator)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _sieveProcessor = sieveProcessor;
            _validator = validator;
        }

        ///<inheritdoc/>
        public async Task<PagedResult<GatewayDto>> GetAll(SieveModel query)
        {
            try
            {
                var gateways = _context.Gateways!.AsNoTracking().Include(x => x.Devices);
                return await _sieveProcessor.GetPagedAsync<Gateway, GatewayDto>(gateways, sieveModel: query, configurationProvider: _mapper.ConfigurationProvider);
            }
            catch (Exception ex)
            {
                var msg = $"Service error while getting {nameof(Gateway).ToString().Humanize(LetterCasing.Title)} list. See the exception for details.";
                _logger.LogError(ex, msg);
                throw new ServiceException(ex.Message);
            }
        }


        ///<inheritdoc/>
        public async Task<GatewayDto> GetById(int id, CancellationToken cancellation)
        {
            try
            {
                var gateway = await _context.Gateways!.AsNoTracking().Where(x => x.Id.Equals(id)).SingleOrDefaultAsync(cancellation);
                if (gateway is null)
                {
                    throw new NotFoundException($"{nameof(Gateway).ToString().Humanize(LetterCasing.Title)} does not exist.");
                }
                _logger.LogInformation($"Get {typeof(Gateway).ToString().Humanize(LetterCasing.Title)}. Id: {id}");
                return _mapper.Map<GatewayDto>(gateway);
            }
            catch (NotFoundException ex)
            {
                var msg = $"Not found error while getting {nameof(Gateway).ToString().Humanize(LetterCasing.Title)}. See the exception for details.";
                _logger.LogError(ex, msg);
                throw ex;
            }
            catch (Exception ex)
            {
                var msg = $"Service error while getting {nameof(Gateway).ToString().Humanize(LetterCasing.Title)}. See the exception for details.";
                _logger.LogError(ex, msg);
                throw new ServiceException(ex.Message);
            }
        }

        ///<inheritdoc/>
        public async Task<GatewayDto> Create(AddGatewayDto dto, CancellationToken cancellation)
        {
            try
            {
                var gateway = _mapper.Map<Gateway>(dto);

                await ValidationUtils.ValidateAndThrow(ValidationUtils.CreateValidationContext(gateway, true, new string[] { ValidationAction.Add.ToString() }), _validator, cancellation);

                var result = await _context.Gateways!.AddAsync(gateway, cancellation);
                await _context.SaveChangesAsync(cancellation);

                _logger.LogInformation($"{nameof(Gateway).ToString().Humanize(LetterCasing.Title)} added. Id: '{gateway!.Id}'");
                return await GetById(result.Entity.Id, cancellation);
            }
            catch (ValidationException ex)
            {
                var msg = $"Validation error while adding {nameof(Gateway).ToString().Humanize(LetterCasing.Title)}. See the exception for details.";
                _logger.LogError(ex, msg);
                throw ex;
            }
            catch (Exception ex)
            {
                var msg = $"Service error while adding {nameof(Gateway).ToString().Humanize(LetterCasing.Title)}. See the exception for details.";
                _logger.LogError(ex, msg);
                throw new ServiceException(ex.Message);
            }
        }

        ///<inheritdoc/>
        public async Task Update(UpdateGatewayDto dto, CancellationToken cancellation)
        {
            try
            {
                var gateway = await _context.Gateways!.Where(x => x.Id.Equals(dto.Id)).SingleOrDefaultAsync(cancellation);
                if (gateway is null)
                {
                    throw new NotFoundException($"{nameof(Gateway).ToString().Humanize(LetterCasing.Title)} does not exist.");
                }

                gateway = _mapper.Map(dto, gateway);

                await ValidationUtils.ValidateAndThrow(ValidationUtils.CreateValidationContext(gateway, true, new string[] { ValidationAction.Update.ToString() }), _validator, cancellation);

                await _context.SaveChangesAsync(cancellation);
                _logger.LogInformation($"{nameof(Gateway).ToString().Humanize(LetterCasing.Title)} updated. Id: '{gateway!.Id}'");
            }
            catch (NotFoundException ex)
            {
                var msg = $"Not found error while updating {nameof(Gateway).ToString().Humanize(LetterCasing.Title)}. See the exception for details.";
                _logger.LogError(ex, msg);
                throw ex;
            }
            catch (ValidationException ex)
            {
                var msg = $"Validation error while updating {nameof(Gateway).ToString().Humanize(LetterCasing.Title)}. See the exception for details.";
                _logger.LogError(ex, msg);
                throw ex;
            }
            catch (Exception ex)
            {
                var msg = $"Service error while updating {nameof(Gateway).ToString().Humanize(LetterCasing.Title)}. See the exception for details.";
                _logger.LogError(ex, msg);
                throw new ServiceException(ex.Message);
            }
        }

        ///<inheritdoc/>
        public async Task Delete(int id, CancellationToken cancellation)
        {
            try
            {
                var gateway = await _context.Gateways!.Where(x => x.Id.Equals(id)).SingleOrDefaultAsync(cancellation);
                if (gateway is null)
                {
                    throw new NotFoundException($"{nameof(Gateway).ToString().Humanize(LetterCasing.Title)} does not exist.");
                }

                _context.Gateways!.Remove(gateway!);
                await _context.SaveChangesAsync(cancellation);
                _logger.LogInformation($"{nameof(Gateway).ToString().Humanize(LetterCasing.Title)} deleted. Id: {id}");
            }
            catch (NotFoundException ex)
            {
                var msg = $"Not found error while deleting {nameof(Gateway).ToString().Humanize(LetterCasing.Title)}. See the exception for details.";
                _logger.LogError(ex, msg);
                throw ex;
            }
            catch (Exception ex)
            {
                var msg = $"Service error while deleting {nameof(Gateway).ToString().Humanize(LetterCasing.Title)}. See the exception for details.";
                _logger.LogError(ex, msg);
                throw new ServiceException(ex.Message);
            }
        }
    }
}
