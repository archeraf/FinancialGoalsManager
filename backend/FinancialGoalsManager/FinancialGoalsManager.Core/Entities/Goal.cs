using FinancialGoalsManager.Core.Enums;

namespace FinancialGoalsManager.Core.Entities
{
    public class Goal : BaseEntity
    {
        private readonly List<Transaction> _transactions;

        private Goal()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
            IsDeleted = false;
            _transactions = new List<Transaction>();
            Status = GoalStatus.InProgress;
        }

        public string Title { get; private set; } = string.Empty;
        public decimal Amount { get; set; }
        public decimal AmountGoal { get; private set; }
        public DateTime Deadline { get; private set; }
        public decimal IdealMonthlyDeposit { get; private set; }
        public GoalStatus Status { get; private set; }
        public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();
        public DateTime CreationDate { get; private set; }
        public bool IsDeleted { get; private set; }


        #region Business methods

        public static Goal Create(string title, decimal amount, decimal amountGoal, DateTime deadline, decimal idealMonthlyDeposit)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be null or empty.", nameof(title));

            if (amountGoal <= 0)
                throw new ArgumentOutOfRangeException(nameof(amountGoal), "Amount goal must be greater than zero.");

            if (idealMonthlyDeposit <= 0)
                throw new ArgumentOutOfRangeException(nameof(idealMonthlyDeposit), "Ideal monthly deposit must be greater than zero.");

            var goal = new Goal
            {
                Title = title.Trim(),
                Amount = amount,
                AmountGoal = amountGoal,
                Deadline = deadline,
                IdealMonthlyDeposit = idealMonthlyDeposit,
                Status = GoalStatus.InProgress
            };

            return goal;
        }

        public void Deposit(decimal amount, DateTime? transactionDate = null)
        {
            EnsureNotDeleted();
            EnsureNotCanceled();

            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Deposit amount must be greater than zero.");

            var transaction = Transaction.Create(this.Id, amount, TransactionType.Deposit, transactionDate ?? DateTime.UtcNow);
            _transactions.Add(transaction);

            CompleteIfTargetReached();
        }

        public void Withdraw(decimal amount, DateTime? transactionDate = null)
        {
            EnsureNotDeleted();
            EnsureNotCanceled();

            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Withdrawal amount must be greater than zero.");

            var t = Transaction.Create(this.Id, amount, TransactionType.Withdraw, transactionDate ?? DateTime.UtcNow);
            _transactions.Add(t);
        }

        public decimal CurrentBalance()
        {
            return _transactions
                .Where(t => !t.IsDeleted)
                .Sum(t => t.SignedAmount());
        }

        public decimal ProgressPercentage()
        {
            if (AmountGoal <= 0) return 0m;
            var progress = (CurrentBalance() / AmountGoal) * 100m;
            if (progress < 0) return 0m;
            return progress >= 100m ? 100m : Math.Round(progress, 2);
        }

        public void Pause()
        {
            EnsureNotDeleted();
            if (Status == GoalStatus.Complete || Status == GoalStatus.Canceled)
                throw new InvalidOperationException("Cannot pause a completed or canceled goal.");

            Status = GoalStatus.Paused;
        }

        public void Resume()
        {
            EnsureNotDeleted();
            if (Status != GoalStatus.Paused)
                throw new InvalidOperationException("Only paused goals can be resumed.");

            Status = GoalStatus.InProgress;
        }

        public void Cancel()
        {
            EnsureNotDeleted();
            if (Status == GoalStatus.Complete)
                throw new InvalidOperationException("Cannot cancel a completed goal.");

            Status = GoalStatus.Canceled;
        }
        private void CompleteIfTargetReached()
        {
            if (Status == GoalStatus.Canceled || Status == GoalStatus.Complete || IsDeleted)
                return;

            if (CurrentBalance() >= AmountGoal)
                Status = GoalStatus.Complete;
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

        #region Data Update Methods
        public void UpdateTitle(string newTitle)
        {
            EnsureNotDeleted();
            if (string.IsNullOrWhiteSpace(newTitle))
                throw new ArgumentException("Title cannot be null or empty.", nameof(newTitle));

            Title = newTitle.Trim();
        }

        public void UpdateAmountGoal(decimal newAmountGoal)
        {
            EnsureNotDeleted();
            if (newAmountGoal <= 0)
                throw new ArgumentOutOfRangeException(nameof(newAmountGoal), "Amount goal must be greater than zero.");

            AmountGoal = newAmountGoal;
            CompleteIfTargetReached();
        }

        public void UpdateDeadline(DateTime newDeadline)
        {
            EnsureNotDeleted();
            Deadline = newDeadline;
        }

        public void UpdateIdealMonthlyDeposit(decimal newIdeal)
        {
            EnsureNotDeleted();
            if (newIdeal <= 0)
                throw new ArgumentOutOfRangeException(nameof(newIdeal), "Ideal monthly deposit must be greater than zero.");

            IdealMonthlyDeposit = newIdeal;
        }
        #endregion

        #region Private Helpers
        private void EnsureNotDeleted()
        {
            if (IsDeleted)
                throw new InvalidOperationException("Operation not allowed on a deleted goal.");
        }

        private void EnsureNotCanceled()
        {
            if (Status == GoalStatus.Canceled)
                throw new InvalidOperationException("Operation not allowed on a canceled goal.");
        }
        #endregion

        public override string ToString()
            => $"Goal '{Title}' {ProgressPercentage():N2}% ({CurrentBalance():N2}/{AmountGoal:N2}) - Status: {Status}";
    }
}