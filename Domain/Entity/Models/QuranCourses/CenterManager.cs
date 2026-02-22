using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Auth;
using Entity.Models.Common;

namespace Entity.Models.QuranCourses;
/// <summary>
/// O'quv markazni boshqaruvchi xodimlar
/// </summary>
[Table("center_managers", Schema = "quran_courses")]
public class CenterManager : AuditableModelBase<long>
{
    [Column("user_id"), ForeignKey(nameof(User))] 
    public long UserId { get; set; }
    public virtual User User { get; set; }
    [Column("training_center_id"), ForeignKey(nameof(TrainingCenter))]
    public long TrainingCenterId { get; set; }
    public virtual TrainingCenter TrainingCenter { get; set; }
}