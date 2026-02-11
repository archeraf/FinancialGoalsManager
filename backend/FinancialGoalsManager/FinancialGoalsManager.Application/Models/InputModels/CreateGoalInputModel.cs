using FinancialGoalsManager.Core.VO;

namespace FinancialGoalsManager.Application.DTO.InputModels
{
    public record CreateGoalInputModel(
        string Title,
        Payment Amount,
        Payment AmountGoal,
        DateTime Deadline,
        Payment IdealMonthlyDeposit
    );
}