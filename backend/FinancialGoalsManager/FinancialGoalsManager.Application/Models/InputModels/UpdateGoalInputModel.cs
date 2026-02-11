using FinancialGoalsManager.Core.Enums;
using FinancialGoalsManager.Core.VO;

namespace FinancialGoalsManager.Application.DTO.InputModels
{
    public record UpdateGoalInputModel(
        Guid Id,
        string Title,
        Payment AmountGoal,
        DateTime Deadline,
        Payment IdealMonthlyDeposit,
        GoalStatus Status = GoalStatus.InProgress
    );
}