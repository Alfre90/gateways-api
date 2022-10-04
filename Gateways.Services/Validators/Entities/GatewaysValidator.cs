using System.IO;
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
        /// <summary>
        /// Main constructor
        /// </summary>
        public GatewaysValidator(GatewaysContext _dbContext)
        {

            RuleSet(nameof(ValidationAction.Add), () =>
            {
                RuleFor(x => x.IPv4).Must(ip =>
                    ip!.Split(".").Length == 4
                            && !ip.Split(".").Any(
                           x =>
                           {
                               int y;
                               return Int32.TryParse(x, out y) && y > 255 || y < 1;
                           })
                    ).WithMessage("Ip Address is invalid.");

                RuleFor(x => x.SerialNumber)
                   .NotNull()
                   .MustAsync(async (number, cancellation) =>
                   {
                       var gateway = await _dbContext.Gateways!.FirstOrDefaultAsync(x => x.SerialNumber!.Equals(number), cancellation);
                       return gateway == null;
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

                RuleFor(x => x.IPv4).Must(ip =>
                   ip!.Split(".").Length == 4
                           && !ip.Split(".").Any(
                          x =>
                          {
                              int y;
                              return Int32.TryParse(x, out y) && y > 255 || y < 1;
                          })
                   ).WithMessage("Ip Address is invalid.");
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
