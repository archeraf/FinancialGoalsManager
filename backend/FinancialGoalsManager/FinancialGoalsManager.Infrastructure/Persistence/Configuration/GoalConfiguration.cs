using FinancialGoalsManager.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialGoalsManager.Infrastructure.Persistence.Configuration
{
    public class GoalConfiguration : IEntityTypeConfiguration<Goal>
    {
        public void Configure(EntityTypeBuilder<Goal> builder)
        {
            //Table name
            builder.ToTable("Goals");

            //Primary Key
            builder.HasKey(g => g.Id);

            //Properties
            builder.Property(g => g.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.HasMany(g => g.Transactions)
                   .WithOne()
                   .HasForeignKey(t => t.GoalId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(g => g.Deadline)
                        .IsRequired();


            builder.Property(g => g.Status)
                        .IsRequired();

            builder.Property(g => g.CreationDate)
                        .IsRequired();

            builder.Property(g => g.IsDeleted)
                        .IsRequired();

            builder.OwnsOne(g => g.Amount, a =>
            {
                a.Property(p => p.Amount)
                 .HasColumnName("AmountValue")
                 .HasColumnType("decimal(18,2)")
                 .IsRequired();
            });

            builder.OwnsOne(g => g.AmountGoal, a =>
            {
                a.Property(p => p.Amount)
                 .HasColumnName("AmountGoalValue")
                 .HasColumnType("decimal(18,2)")
                 .IsRequired();
            });

            builder.OwnsOne(g => g.IdealMonthlyDeposit, a =>
            {
                a.Property(p => p.Amount)
                 .HasColumnName("IdealMonthlyDepositValue")
                 .HasColumnType("decimal(18,2)")
                 .IsRequired();
            });

        }
    }
}
