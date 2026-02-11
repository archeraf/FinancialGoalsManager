using FinancialGoalsManager.Core.Enums;
using FinancialGoalsManager.Core.VO;

namespace FinancialGoalsManager.Core.Entities
{
    public class Transaction : BaseEntity
    {
        private Transaction()
        {
            Id = Guid.NewGuid();
            TransactionDate = DateTime.UtcNow;
            IsDeleted = false;
            GoalId = Guid.Empty;
            Amount = new Payment(0m, "BRL");
        }

        public Payment Amount { get; private set; }
        public TransactionType Type { get; private set; }
        public DateTime TransactionDate { get; private set; }
        public bool IsDeleted { get; private set; }
        public Guid GoalId { get; set; }

        #region Builders
        public static Transaction Create(Guid goalId, Payment amount, TransactionType type, DateTime transactionDate)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");
            }

            if (transactionDate == default)
            {
                transactionDate = DateTime.UtcNow;
            }

            return new Transaction
            {
                GoalId = goalId,
                Amount = amount,
                Type = type,
                TransactionDate = transactionDate
            };
        }

        #endregion

        #region Status Management

        public void MarkAsDeleted()
        {
            IsDeleted = true;
        }

        public void Restore()
        {
            IsDeleted = false;
        }

        #endregion

        #region Helpers

        public bool IsDeposit() => Type == TransactionType.Deposit;
        public bool IsWithdraw() => Type == TransactionType.Withdraw;

        public decimal SignedAmount() => IsDeposit() ? Amount : -Amount;

        #endregion

        public override string ToString()
            => $"{Type} {Amount:N2} on {TransactionDate:O} (Id: {Id})";
    }
}