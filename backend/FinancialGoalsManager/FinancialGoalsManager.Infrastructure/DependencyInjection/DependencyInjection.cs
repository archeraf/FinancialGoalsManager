using FinancialGoalsManager.Core.Contracts.Repository.Generic;
using FinancialGoalsManager.Infrastructure.Persistence.Context;
using FinancialGoalsManager.Infrastructure.Persistence.Repository.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialGoalsManager.Infrastructure.Persistence.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<SqlServerContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            return services;
        }
    }
}
