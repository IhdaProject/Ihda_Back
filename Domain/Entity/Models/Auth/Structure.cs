using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models;

[Table("structures", Schema = "auth")]
public class Structure : AuditableModelBase<long>
{
    [Column("name")] public string Name { get; set; }
    public virtual ICollection<StructurePermission> StructurePermissions { get; set; }
    [Column("is_default")] public bool IsDefault { get; set; }
}