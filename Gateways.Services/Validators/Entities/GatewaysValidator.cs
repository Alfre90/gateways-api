using System.Text.RegularExpressions;
using FluentValidation;
using Gateways.Core.Entities;
using Gateways.Database;
using Gateways.Services.Common.Validations;
using Microsoft.EntityFrameworkCore;

namespace Gateways.Services.Validators.Entities
{
    /// <summary>
    /// Gateways Validator
    /// </summary>
    public class GatewaysValidator : AbstractValidator<Gateway>
    {
        string regexIpv4 ="(([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])\\.){3}([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])";
        /// <summary>
        /// Main constructor
        /// </summary>
        public GatewaysValidator(GatewaysContext _dbContext)
        {
            RuleFor(x => x.IPv4).Matches(regexIpv4);

            RuleSet(nameof(ValidationAction.Add), () =>
            {
                RuleFor(x => x.SerialNumber)
                   .NotNull()
                   .MustAsync(async (number, cancellation) =>
                   {
                       return await _dbContext.Gateways!.AnyAsync(x => !x.SerialNumber!.Equals(number), cancellation);
                   }).WithMessage("Gateway with this serial number already exist.");
            });

            RuleSet(nameof(ValidationAction.Update), () =>
            {
                RuleFor(x => x.Id)
                   .NotNull()
                   .MustAsync(async (id, cancellation) =>
                   {
                       return await _dbContext.Gateways!.AnyAsync(x => x.Id.Equals(id), cancellation);
                   }).WithMessage("Gateway does not exist.");
            });

            RuleSet(nameof(ValidationAction.Delete), () =>
            {
                RuleFor(x => x.Id)
                   .NotNull()
                   .MustAsync(async (id, cancellation) =>
                   {
                       return await _dbContext.Gateways!.AnyAsync(x => x.Id.Equals(id), cancellation);
                   }).WithMessage("Gateway does not exist.");
            });
        }
    }
}
