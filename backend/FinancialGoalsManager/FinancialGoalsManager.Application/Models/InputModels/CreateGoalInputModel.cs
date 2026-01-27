namespace FinancialGoalsManager.Application.DTO.InputModels
{
    public record CreateGoalInputModel(
        string Title,
        decimal AmountGoal,
        DateTime? Deadline = null,
        decimal? IdealMonthlyDeposit = null
    );
}