using FinancialGoalsManager.Application.DTO.InputModels;
using FinancialGoalsManager.Application.DTO.ViewModels;
using FinancialGoalsManager.Application.Services.Contracts;
using FinancialGoalsManager.Core.Contracts.Repository.Generic;
using FinancialGoalsManager.Core.Entities;

namespace FinancialGoalsManager.Application.Services
{
    public class GoalServiceImplementation : IGoalsService
    {
        private readonly IRepository<Goal> _goalRepository;
        public GoalServiceImplementation(IRepository<Goal> goalRepository)
        {
            _goalRepository = goalRepository;
        }


        public async Task<GoalViewModel> GetGoalByIdAsync(Guid goalId)
        {
            var goalEntity = await _goalRepository.GetByIdAsync(goalId, p => p.Transactions);
            if (goalEntity == null)
            {
                throw new Exception("Goal not found.");
            }
            return new GoalViewModel(
                goalEntity.Id,
                goalEntity.Title,
                goalEntity.Amount,
                goalEntity.AmountGoal,
                goalEntity.Deadline,
                goalEntity.IdealMonthlyDeposit,
                goalEntity.Status,
                goalEntity.Transactions.Select(t => new TransactionViewModel(
                    t.Id,
                    t.Amount,
                    t.Type,
                    t.TransactionDate
                    )).ToList(),
                goalEntity.CreationDate,
                goalEntity.IsDeleted,
                goalEntity.CurrentBalance(),
                goalEntity.ProgressPercentage()
                );
        }

        public async Task<IEnumerable<GoalViewModel>> GetGoalsAsync()
        {
            var goals = await _goalRepository.GetAllASync(g => g.Transactions);

            return goals.Select(goal => new GoalViewModel(
                goal.Id,
                goal.Title,
                goal.Amount,
                goal.AmountGoal,
                goal.Deadline,
                goal.IdealMonthlyDeposit,
                goal.Status,
                goal.Transactions.Select(t => new TransactionViewModel(
                    t.Id,
                    t.Amount,
                    t.Type,
                    t.TransactionDate
                    )).ToList(),
                goal.CreationDate,
                goal.IsDeleted,
                goal.CurrentBalance(),
                goal.ProgressPercentage()
                )).Where(g => !g.IsDeleted);
        }


        public async Task<GoalViewModel> CreateGoalAsync(CreateGoalInputModel goal)
        {
            var goalEntity = Goal.Create(goal.Title, goal.Amount, goal.AmountGoal, goal.Deadline, goal.IdealMonthlyDeposit);
            await _goalRepository.AddAsync(goalEntity);

            if (await _goalRepository.SaveChangesAsync())
            {
                return new GoalViewModel(
                    goalEntity.Id,
                    goalEntity.Title,
                    goalEntity.Amount,
                    goalEntity.AmountGoal,
                    goalEntity.Deadline,
                    goalEntity.IdealMonthlyDeposit,
                    goalEntity.Status,
                    new List<TransactionViewModel>(),
                    goalEntity.CreationDate,
                    goalEntity.IsDeleted,
                    goalEntity.CurrentBalance(),
                    goalEntity.ProgressPercentage()
                    );
            }

            throw new Exception("Failed to create goal.");
        }

        public async Task DeleteGoalAsync(Guid goalId)
        {
            var goal = await _goalRepository.GetByIdAsync(goalId);
            if (goal == null)
            {
                throw new Exception("Goal not found.");
            }
            goal.MarkAsDeleted();
            await _goalRepository.UpdateAsync(goal);
            if (!await _goalRepository.SaveChangesAsync())
            {
                throw new Exception("Failed to delete goal.");
            }
        }

        public async Task<GoalViewModel> UpdateGoalAsync(UpdateGoalInputModel goal)
        {
            var goalEntity = await _goalRepository.GetByIdAsync(goal.Id);
            if (goalEntity == null)
            {
                throw new Exception("Goal not found.");
            }

            goalEntity.UpdateTitle(goal.Title);
            goalEntity.UpdateAmountGoal(goal.AmountGoal);
            goalEntity.UpdateDeadline(goal.Deadline);
            goalEntity.UpdateIdealMonthlyDeposit(goal.IdealMonthlyDeposit);

            await _goalRepository.UpdateAsync(goalEntity);

            if (await _goalRepository.SaveChangesAsync())
            {
                return new GoalViewModel(
                    goalEntity.Id,
                    goalEntity.Title,
                    goalEntity.Amount,
                    goalEntity.AmountGoal,
                    goalEntity.Deadline,
                    goalEntity.IdealMonthlyDeposit,
                    goalEntity.Status,
                    goalEntity.Transactions.Select(t => new TransactionViewModel(
                        t.Id,
                        t.Amount,
                        t.Type,
                        t.TransactionDate
                        )).ToList(),
                    goalEntity.CreationDate,
                    goalEntity.IsDeleted,
                    goalEntity.CurrentBalance(),
                    goalEntity.ProgressPercentage()
                    );
            }

            throw new Exception("Failed to update goal.");
        }
    }
}
