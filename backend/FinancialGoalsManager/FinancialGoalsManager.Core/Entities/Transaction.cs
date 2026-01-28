using FinancialGoalsManager.Core.Enums;

namespace FinancialGoalsManager.Core.Entities
{
    public class Transaction : BaseEntity
    {
        private Transaction()
        {
            Id = Guid.NewGuid();
            TransactionDate = DateTime.UtcNow;
            IsDeleted = false;
        }

        public decimal Amount { get; private set; }
        public TransactionType Type { get; private set; }
        public DateTime TransactionDate { get; private set; }
        public bool IsDeleted { get; private set; }

        #region Builders
        public static Transaction Create(decimal amount, TransactionType type, DateTime transactionDate)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");

            if (transactionDate == default)
                transactionDate = DateTime.UtcNow;

            return new Transaction
            {
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