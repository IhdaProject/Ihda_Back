using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models.Common;

public abstract class AuditableModelBase<TId> : ModelBase<TId>
{
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    [Column("created_by")]
    public long CreatedBy { get; set; }
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
    [Column("updated_by")]
    public long UpdatedBy { get; set; }
}