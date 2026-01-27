using FinancialGoalsManager.Application.Services;
using FinancialGoalsManager.Application.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialGoalsManager.Infrastructure.Persistence.DependencyInjection
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddApplication(this IServiceCollection service)
        {
            //    service.AddFluentValidationAutoValidation()
            //        .AddValidatorsFromAssemblyContaining<CreateProjectRequest>();

            //service.AddScoped<IValidator<CreateProjectRequest>, CreateProjectRequestValidator>();
            service.AddScoped<IGoalsService, GoalServiceImplementation>();
            return service;
        }
    }
}
