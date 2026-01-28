using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialGoalsManager.Core.Entities
{
    public class BaseEntity
    {
        [Column("id")]
        public Guid Id { get; set; }
    }
}
