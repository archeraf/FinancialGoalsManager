using FinancialGoalsManager.Application.DTO.InputModels;
using FluentValidation;

namespace FinancialGoalsManager.Application.Validators
{
    public class UpdateGoalInputModelValidator : AbstractValidator<UpdateGoalInputModel>
    {
        public UpdateGoalInputModelValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id is required.");

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required.")
                .MinimumLength(3)
                .WithMessage("Title must be at least 3 characters long.")
                .MaximumLength(200)
                .WithMessage("Title must not exceed 200 characters.");

            RuleFor(x => x.AmountGoal)
                .GreaterThan(0)
                .WithMessage("AmountGoal must be greater than zero.")
                .PrecisionScale(18, 2, ignoreTrailingZeros: true)
                .WithMessage("AmountGoal must have at most 2 decimal places.");

            RuleFor(x => x.Deadline)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("Deadline must be in the future.");

            RuleFor(x => x.IdealMonthlyDeposit)
                .GreaterThan(0)
                .WithMessage("IdealMonthlyDeposit must be greater than zero.")
                .PrecisionScale(18, 2, ignoreTrailingZeros: true)
                .WithMessage("IdealMonthlyDeposit must have at most 2 decimal places.");

            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Status must be a valid value.");
        }
    }
}