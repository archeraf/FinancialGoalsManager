using System;
using FinancialGoalsManager.Core.Enums;

namespace FinancialGoalsManager.Application.DTO.InputModels
{
    public record UpdateGoalInputModel(
        Guid Id,
        string Title,
        decimal AmountGoal,
        DateTime? Deadline = null,
        decimal? IdealMonthlyDeposit = null,
        GoalStatus Status = GoalStatus.InProgress
    );
}