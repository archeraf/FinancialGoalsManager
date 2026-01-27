using FinancialGoalsManager.Application.DTO.InputModels;
using FinancialGoalsManager.Application.DTO.ViewModels;
using FinancialGoalsManager.Application.Services.Contracts;

namespace FinancialGoalsManager.Application.Services
{
    public class TransactionServiceImplementation : ITransactionService
    {
        public Task<TransactionViewModel> CreateTransaction(CreateTranscationInputModel transaction)
        {
            throw new NotImplementedException();
        }


        public void DeleteTransaction(Guid transactionId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TransactionViewModel>> GetTransactions()
        {
            throw new NotImplementedException();
        }

        public Task<TransactionViewModel> UpdateTransaction(UpdateTranscationInputModel transaction)
        {
            throw new NotImplementedException();
        }

    }
}
