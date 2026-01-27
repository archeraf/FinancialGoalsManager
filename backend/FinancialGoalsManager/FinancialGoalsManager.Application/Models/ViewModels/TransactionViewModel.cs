using FinancialGoalsManager.Core.Enums;

namespace FinancialGoalsManager.Application.DTO.ViewModels
{
    public record TransactionViewModel(
        Guid Id,
        decimal Amount,
        TransactionType Type,
        DateTime TransactionDate
    );
}