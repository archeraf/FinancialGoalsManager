using FinancialGoalsManager.Core.Enums;

namespace FinancialGoalsManager.Application.DTO.ViewModels
{
    public record GoalViewModel(
        Guid Id,
        string Title,
        decimal AmountGoal,
        DateTime? Deadline,
        decimal? IdealMonthlyDeposit,
        GoalStatus Status,
        IReadOnlyList<TransactionViewModel> Transactions,
        DateTime CreationDate,
        bool IsDeleted,
        decimal CurrentBalance,
        decimal ProgressPercentage
    );
}