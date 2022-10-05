using System.Text.RegularExpressions;
using FluentValidation;
using Gateways.Core.Entities;
using Gateways.Database;
using Gateways.Services.Common.Validations;
using Microsoft.EntityFrameworkCore;

namespace Gateways.Services.Validators.Entities
{
    /// <summary>
    /// Devices Validator
    /// </summary>
    public class DevicesValidator : AbstractValidator<Device>
    {
        /// <summary>
        /// Main constructor
        /// </summary>
        public DevicesValidator(GatewaysContext _dbContext)
        {

            RuleSet(nameof(ValidationAction.Add), () =>
            {
                RuleFor(x => x.GatewayId)
                   .NotNull()
                   .MustAsync(async (id, cancellation) =>
                   {
                       return await _dbContext.Gateways!.AnyAsync(x => x.Id.Equals(id), cancellation);
                   }).WithMessage("Gateway does not exist.");
            });

            RuleSet(nameof(ValidationAction.Update), () =>
            {
                RuleFor(x => x.Uid)
                   .NotNull()
                   .MustAsync(async (id, cancellation) =>
                   {
                       return await _dbContext.Devices!.AnyAsync(x => x.Uid.Equals(id), cancellation);
                   }).WithMessage("Device does not exist.");
            });

            RuleSet(nameof(ValidationAction.Delete), () =>
            {
                RuleFor(x => x.Uid)
                   .NotNull()
                   .MustAsync(async (id, cancellation) =>
                   {
                       return await _dbContext.Devices!.AnyAsync(x => x.Uid.Equals(id), cancellation);
                   }).WithMessage("Device does not exist.");
            });
        }
    }
}
