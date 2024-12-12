using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Auth;
using Entity.Models.Common;

namespace Entity.Models
{
    [Table("structure_permissions", Schema = "auth")]
    public class StructurePermission : AuditableModelBase<long>
    {
        [Column("structure_id"), ForeignKey(nameof(Structure))]
        public long StructureId { get; set; }

        public virtual Structure Structure { get; set; }

        [Column("permission_id")]
        [ForeignKey("Permission")]
        public long PermissionId { get; set; }

        public virtual Permission Permission { get; set; }

        [Column("granted_by_id"), ForeignKey(nameof(GrantedBy))]
        public long GrantedById { get; set; }

        [NotMapped]public virtual User GrantedBy { get; set; }
    }
}