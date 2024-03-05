using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models;

public abstract class AuditableModelBase<TId> : ModelBase<TId>
{
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}