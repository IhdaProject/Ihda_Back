using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Auth;
using Entity.Models.Common;

namespace Entity.Models.Mosques;

public class MosqueAdmin : AuditableModelBase<long>
{
    /// <summary>
    /// qaysi masjidga bog'langanligi
    /// </summary>
    [Column("course_form_id"),ForeignKey(nameof(Mosque))] public long MosqueId { get; set; }
    public virtual Mosque Mosque { get; set; }
    /// <summary>
    /// aynan qaysi foydalanuvchiligi
    /// </summary>
    [Column("user_id"),ForeignKey(nameof(User))] public long UserId { get; set; }
    public virtual User User { get; set; }
}