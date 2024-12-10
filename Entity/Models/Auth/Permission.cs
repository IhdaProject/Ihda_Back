using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models.Auth;

[Table("permissions", Schema = "auth"), Index(nameof(Code), IsUnique = true)]
public class Permission : ModelBase<long>
{
    [Column("name")] public MultiLanguageField Name { get; set; }
    [Column("code")] public int Code { get; set; }
}