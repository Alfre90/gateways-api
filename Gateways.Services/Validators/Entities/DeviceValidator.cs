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
