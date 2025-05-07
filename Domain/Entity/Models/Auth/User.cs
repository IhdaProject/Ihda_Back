using System.ComponentModel.DataAnnotations.Schema;
using Entity.Enums;
using Entity.Models.Common;

namespace Entity.Models.Auth;
[Table("users", Schema = "auth")]
public class User : AuditableModelBase<long>
{
    [Column("full_name")] public string FullName { get; set; }
    [Column("birth_date")] public DateTime? BirthDate { get; set; }
    [Column("gender")] public Gender Gender { get; set; } = Gender.NoSelect;
    [Column("pinfl")] public string? Pinfl { get; set; }
    [NotMapped] public virtual IEnumerable<SignMethod> SignMethods { get; set; }
    [Column("structure_id"), ForeignKey("Structure")]
    public long? StructureId { get; set; }
    public virtual Structure? Structure { get; set; }
}