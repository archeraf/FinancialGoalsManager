using FinancialGoalsManager.Core.Enums;

namespace FinancialGoalsManager.Application.DTO.InputModels
{
    public record UpdateGoalInputModel(
        Guid Id,
        string Title,
        decimal AmountGoal,
        DateTime Deadline,
        decimal IdealMonthlyDeposit,
        GoalStatus Status = GoalStatus.InProgress
    );
}