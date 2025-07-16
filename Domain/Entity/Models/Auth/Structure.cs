using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models.Auth;

[Table("structures", Schema = "auth")]
public class Structure : AuditableModelBase<long>
{
    [Column("name")] public string Name { get; set; }
    public virtual ICollection<StructurePermission> StructurePermissions { get; set; }
    /// <summary>
    /// 1 default, 2 superAdmin
    /// </summary>
    [Column("type")] public int Type { get; set; }
}