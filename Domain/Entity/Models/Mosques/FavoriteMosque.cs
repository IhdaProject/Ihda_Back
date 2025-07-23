using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Auth;
using Entity.Models.Common;

namespace Entity.Models.Mosques;
[Table("favorite_mosques",Schema = "prayerful")]
public class FavoriteMosque : AuditableModelBase<long>
{
    [Column("user_id"), ForeignKey(nameof(User))] public long UserId { get; set; }
    public virtual User User { get; set; }
    [Column("mosque_id"), ForeignKey(nameof(Mosque))] public long MosqueId { get; set; }
    public virtual Mosque Mosque { get; set; }
}