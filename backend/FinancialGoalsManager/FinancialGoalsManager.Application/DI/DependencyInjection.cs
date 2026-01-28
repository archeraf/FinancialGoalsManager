using FinancialGoalsManager.Application.DTO.InputModels;
using FinancialGoalsManager.Application.Services;
using FinancialGoalsManager.Application.Services.Contracts;
using FinancialGoalsManager.Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialGoalsManager.Infrastructure.Persistence.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection service)
        {
            // Register validators
            service.AddScoped<IValidator<CreateGoalInputModel>, CreateGoalInputModelValidator>();
            service.AddScoped<IValidator<UpdateGoalInputModel>, UpdateGoalInputModelValidator>();
            service.AddScoped<IValidator<CreateTranscationInputModel>, CreateTranscationInputModelValidator>();
            service.AddScoped<IValidator<UpdateTranscationInputModel>, UpdateTranscationInputModelValidator>();

            // Register services
            service.AddScoped<IGoalsService, GoalServiceImplementation>();
            service.AddScoped<ITransactionService, TransactionServiceImplementation>();

            return service;
        }
    }
}
