namespace FinancialGoalsManager.Application.DTO.InputModels
{
    public record CreateGoalInputModel(
        string Title,
        decimal Amount,
        decimal AmountGoal,
        DateTime Deadline,
        decimal IdealMonthlyDeposit
    );
}