using FinancialGoalsManager.Application.DTO.InputModels;
using FinancialGoalsManager.Application.DTO.ViewModels;

namespace FinancialGoalsManager.Application.Services.Contracts
{
    public interface ITransactionService
    {
        public Task<IEnumerable<TransactionViewModel>> GetTransactions();
        public Task<TransactionViewModel> GetTransactionsById(Guid id);
        public Task<TransactionViewModel> CreateTransaction(CreateTranscationInputModel transaction);
        public Task<TransactionViewModel> UpdateTransaction(UpdateTranscationInputModel transaction);
        public Task DeleteTransaction(Guid transactionId);

    }
}
