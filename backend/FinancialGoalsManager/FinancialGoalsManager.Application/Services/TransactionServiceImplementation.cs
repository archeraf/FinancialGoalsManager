using FinancialGoalsManager.Application.DTO.InputModels;
using FinancialGoalsManager.Application.DTO.ViewModels;
using FinancialGoalsManager.Application.Services.Contracts;
using FinancialGoalsManager.Core.Contracts.Repository.Generic;
using FinancialGoalsManager.Core.Entities;

namespace FinancialGoalsManager.Application.Services
{
    public class TransactionServiceImplementation : ITransactionService
    {
        private readonly IRepository<Transaction> _transactionRepository;

        public TransactionServiceImplementation(IRepository<Transaction> transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }


        public async Task<IEnumerable<TransactionViewModel>> GetTransactions()
        {
            var transactions = await _transactionRepository.GetAllASync();

            if (transactions == null || !transactions.Any())
            {
                return Enumerable.Empty<TransactionViewModel>();
            }

            return transactions.Select(t => new TransactionViewModel(
                t.Id,
                t.Amount,
                t.Type,
                t.TransactionDate
                ));
        }

        public async Task<TransactionViewModel> GetTransactionsById(Guid id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);

            if (transaction == null)
            {
                throw new Exception("Transaction not found.");
            }

            return new TransactionViewModel(
                transaction.Id,
                transaction.Amount,
                transaction.Type,
                transaction.TransactionDate
                );
        }

        public async Task<TransactionViewModel> CreateTransaction(CreateTranscationInputModel transaction)
        {
            var transactionEntity = Transaction.Create(
                transaction.GoalId ?? Guid.Empty,
                transaction.Amount,
                transaction.Type,
                transaction.TransactionDate ?? DateTime.UtcNow
                );

            await _transactionRepository.AddAsync(transactionEntity);
            if (await _transactionRepository.SaveChangesAsync())
            {
                return new TransactionViewModel(
                    transactionEntity.Id,
                    transactionEntity.Amount,
                    transactionEntity.Type,
                    transactionEntity.TransactionDate
                    );
            }

            throw new Exception("Failed to create transaction.");

        }




        public async Task<TransactionViewModel> UpdateTransaction(UpdateTranscationInputModel transaction)
        {
            var transactionEntity = await _transactionRepository.GetByIdAsync(transaction.Id);

            if (transactionEntity == null)
            {
                throw new Exception("Transaction not found.");
            }

            await _transactionRepository.UpdateAsync(transactionEntity);

            if (await _transactionRepository.SaveChangesAsync())
            {
                return new TransactionViewModel(
                    transactionEntity.Id,
                    transactionEntity.Amount,
                    transactionEntity.Type,
                    transactionEntity.TransactionDate
                    );
            }

            throw new Exception("Failed to update transaction.");

        }
        public async Task DeleteTransaction(Guid transactionId)
        {
            var transactionEntity = await _transactionRepository.GetByIdAsync(transactionId);
            if (transactionEntity == null)
            {
                throw new Exception("Transaction not found.");
            }
            transactionEntity.MarkAsDeleted();
            await _transactionRepository.UpdateAsync(transactionEntity);
            if (!await _transactionRepository.SaveChangesAsync())
            {
                throw new Exception("Failed to delete transaction.");
            }
        }

    }
}
