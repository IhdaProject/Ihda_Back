using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models.Clouds;

[Table("files",  Schema = "cloud")]
public class File : AuditableModelBase<long>
{
    [Column("objname")] public string ObjectName { get; set; }
}