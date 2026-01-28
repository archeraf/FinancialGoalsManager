using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FinancialGoalsManager.API.Filters
{
    public class ValidateModelFilter : IAsyncActionFilter
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidateModelFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            foreach (var argument in context.ActionArguments.Values)
            {
                if (argument == null)
                    continue;

                var argumentType = argument.GetType();
                var validatorType = typeof(IValidator<>).MakeGenericType(argumentType);

                if (_serviceProvider.GetService(validatorType) is IValidator validator)
                {
                    var validationResult = await validator.ValidateAsync(
                        new ValidationContext<object>(argument));

                    if (!validationResult.IsValid)
                    {
                        var errors = validationResult.Errors
                            .GroupBy(e => e.PropertyName)
                            .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

                        context.Result = new BadRequestObjectResult(new
                        {
                            message = "Validation failed",
                            errors = errors
                        });

                        return;
                    }
                }
            }

            await next();
        }
    }
}