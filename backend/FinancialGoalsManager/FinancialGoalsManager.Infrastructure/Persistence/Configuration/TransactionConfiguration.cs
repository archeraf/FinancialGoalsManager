using FinancialGoalsManager.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialGoalsManager.Infrastructure.Persistence.Configuration
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            // Table name
            builder.ToTable("Transactions");

            // Primary Key
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
                   .ValueGeneratedOnAdd();


            // Properties
            builder.Property(t => t.TransactionDate)
                   .IsRequired();

            builder.Property(t => t.IsDeleted)
                   .IsRequired();

            builder.Property(t => t.Type)
                   .IsRequired();


            //Payment mapping as owned entity
            builder.OwnsOne(t => t.Amount, a =>
            {
                a.Property(p => p.Currency)
                .HasColumnName("AmountCurrency")
                .HasMaxLength(3) // ISO 4217 currency code
                .IsRequired();

                a.Property(p => p.Amount)
                .HasColumnName("AmountValue")
                .HasColumnType("decimal(18,2)") // Precision for financial amounts
                .HasMaxLength(18) // 16 digits + decimal point + 2 decimal places
                .IsRequired();
            });

        }
    }
}
