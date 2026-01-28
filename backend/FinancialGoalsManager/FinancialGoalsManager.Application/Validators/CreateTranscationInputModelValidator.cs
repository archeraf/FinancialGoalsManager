using FinancialGoalsManager.Application.DTO.InputModels;
using FluentValidation;

namespace FinancialGoalsManager.Application.Validators
{
    public class CreateTranscationInputModelValidator : AbstractValidator<CreateTranscationInputModel>
    {
        public CreateTranscationInputModelValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Amount must be greater than zero.")
                .PrecisionScale(18, 2, ignoreTrailingZeros: true)
                .WithMessage("Amount must have at most 2 decimal places.");

            RuleFor(x => x.Type)
                .IsInEnum()
                .WithMessage("Type must be either Deposit or Withdraw.");

            RuleFor(x => x.TransactionDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("TransactionDate cannot be in the future.")
                .When(x => x.TransactionDate.HasValue);
        }
    }
}