using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models.Auth;
[Table("user_structures", Schema = "auth")]
public class UserStructure : AuditableModelBase<long>
{
    [Column("user_id"), ForeignKey(nameof(User))]
    public long UserId { get; set; }
    public virtual User User { get; set; }
    [Column("structure_id"), ForeignKey("Structure")]
    public long StructureId { get; set; }
    public virtual Structure Structure { get; set; }
}