using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace Gateways.Services.Common.Validations
{
    /// <summary>
    /// Validation utility methods
    /// </summary>
    public static class ValidationUtils
    {
        /// <summary>
        /// Validate an entity with a given validator.
        ///
        /// Throws an exception if validation fails. 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="context">Validation context</param>
        /// <param name="validator">Entity validator</param>
        /// <param name="cancellation">Cancellation token</param>
        /// <returns></returns>
        public static async Task<IList<ValidationFailure>> Validate<TEntity>(ValidationContext<TEntity> context, IValidator<TEntity> validator, CancellationToken cancellation = default) where TEntity : class => validator != null ? (await validator.ValidateAsync(context, cancellation)).Errors : null!;

        /// <summary>
        /// Validate an entity with a given validator.
        ///
        /// Throws an exception if validation fails. 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="context">Validation context</param>
        /// <param name="validator">Entity validator</param>
        /// <param name="cancellation">Cancellation token</param>
        /// <returns></returns>
        public static async Task ValidateAndThrow<TEntity>(ValidationContext<TEntity> context, IValidator<TEntity> validator, CancellationToken cancellation = default) where TEntity : class
        {
            var validationFailureList = await Validate(context, validator, cancellation);
            if (validationFailureList != null && validationFailureList.Count != 0)
            {
                throw new ValidationException("Validation error. See Errors for details.", validationFailureList);
            }
        }

        /// <summary>
        /// Creates a validation context
        /// </summary>
        /// <param name="entity">Entity to validate</param>
        /// <param name="includeRulesNotInRuleSet">Whether to include rules outside rules sets or not</param>
        /// <param name="ruleSets">Rules sets</param>
        /// <returns>ValidationContext</returns>
        public static ValidationContext<TEntity> CreateValidationContext<TEntity>(TEntity entity,
            bool includeRulesNotInRuleSet, params string[] ruleSets) where TEntity : class
        {
            var context = ValidationContext<TEntity>.CreateWithOptions(entity, (vs) =>
            {
                vs.IncludeRuleSets(ruleSets);
                if (includeRulesNotInRuleSet)
                {
                    vs.IncludeRulesNotInRuleSet();
                }
            });

            return context;
        }
    }
}
