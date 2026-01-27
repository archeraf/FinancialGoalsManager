using FinancialGoalsManager.Core.Enums;

namespace FinancialGoalsManager.Application.DTO.InputModels
{
    public record UpdateTranscationInputModel(
        Guid Id,
        decimal Amount,
        TransactionType Type,
        DateTime? TransactionDate = null,
        bool IsDeleted = false,
        Guid? GoalId = null
    );
}