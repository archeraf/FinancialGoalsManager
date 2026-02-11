using FinancialGoalsManager.Core.Enums;
using FinancialGoalsManager.Core.VO;

namespace FinancialGoalsManager.Application.DTO.InputModels
{
    public record CreateTranscationInputModel(
        Payment Amount,
        TransactionType Type,
        DateTime? TransactionDate = null,
        Guid? GoalId = null
    );
}