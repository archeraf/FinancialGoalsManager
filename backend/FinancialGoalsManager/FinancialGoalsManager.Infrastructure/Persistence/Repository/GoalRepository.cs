using FinancialGoalsManager.Core.Contracts.Repository;
using FinancialGoalsManager.Core.Entities;
using FinancialGoalsManager.Infrastructure.Persistence.Context;
using FinancialGoalsManager.Infrastructure.Persistence.Repository.Generic;

namespace FinancialGoalsManager.Infrastructure.Persistence.Repository
{
    internal class GoalRepository : GenericRepository<Goal>, IGoalRepository
    {
        public GoalRepository(SqlServerContext context) : base(context) { }
        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Goal> GetGoalByTitle(string title)
        {
            throw new NotImplementedException();
        }
    }
}
