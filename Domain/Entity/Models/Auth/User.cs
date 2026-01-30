using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entity.Enums;
using Entity.Models.Common;

namespace Entity.Models.Auth;
[Table("users", Schema = "auth")]
public class User : AuditableModelBase<long>
{
    [Column("full_name")] public string FullName { get; set; }
    [Column("birth_date")] public DateTime? BirthDate { get; set; }
    [Column("gender")] public Gender Gender { get; set; }
    [Column("pinfl")] public string? Pinfl { get; set; }
    [Column("avatarurl")]public string AvatarUrl { get; set; } = string.Empty;
    [NotMapped] public virtual IEnumerable<SignMethod> SignMethods { get; set; }
    public virtual ICollection<UserStructure> Structures { get; set; }
}