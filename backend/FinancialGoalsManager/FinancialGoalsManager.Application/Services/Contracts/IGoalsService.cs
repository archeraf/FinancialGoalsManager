
using FinancialGoalsManager.Application.DTO.InputModels;
using FinancialGoalsManager.Application.DTO.ViewModels;

namespace FinancialGoalsManager.Application.Services.Contracts
{
    public interface IGoalsService
    {
        public Task<IEnumerable<GoalViewModel>> GetGoalsAsync();
        public Task<GoalViewModel> GetGoalByIdAsync(Guid goalId);
        public Task<GoalViewModel> CreateGoalAsync(CreateGoalInputModel goal);
        public Task<GoalViewModel> UpdateGoalAsync(UpdateGoalInputModel goal);
        public Task DeleteGoalAsync(Guid goalId);

    }
}
