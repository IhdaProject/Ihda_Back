using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models.Common;

public abstract class ModelBase<TId>
{
    [Column("id")] public TId Id { get; set; }
    [Column("is_delete")] public bool IsDelete { get; set; } = false;
}