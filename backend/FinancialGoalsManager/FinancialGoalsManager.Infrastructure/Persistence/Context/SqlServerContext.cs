using FinancialGoalsManager.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinancialGoalsManager.Infrastructure.Persistence.Context
{
    public class SqlServerContext : DbContext
    {
        public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options)
        {
        }

        public DbSet<Transaction> Transacoes => Set<Transaction>();
        public DbSet<Goal> Goals => Set<Goal>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Goal>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired();
                entity.HasMany(e => e.Transactions).WithOne().HasForeignKey("GoalId");
            });
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Amount).IsRequired();
                entity.Property(e => e.Type).IsRequired();
                entity.Property(e => e.TransactionDate).IsRequired();
            });
        }
    }
}
