using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialGoalsManager.Core.Entities
{
    public class BaseEntity
    {
        [Column("id")]
        public Guid Id { get; protected set; }
        [Column("creation_date")]
        public DateTime CreationDate { get; protected set; }
        [Column("isDeleted")]
        public bool IsDeleted { get; protected set; }
    }
}
