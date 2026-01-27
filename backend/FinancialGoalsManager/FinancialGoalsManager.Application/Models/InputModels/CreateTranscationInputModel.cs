using FinancialGoalsManager.Core.Enums;

namespace FinancialGoalsManager.Application.DTO.InputModels
{
    public record CreateTranscationInputModel(
        decimal Amount,
        TransactionType Type,
        DateTime? TransactionDate = null,
        Guid? GoalId = null
    );
}