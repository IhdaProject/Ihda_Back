using System.ComponentModel.DataAnnotations.Schema;
using Entity.Enums;
using Entity.Models.Auth;
using Entity.Models.Common;

namespace Entity.Models.QuranCourses;
/// <summary>
/// O'quv markaz Kurs formasida o'qish uchun topshirilgan arizaq
/// </summary>
[Table("status_history_petition", Schema = "quran_courses")]
public class StatusHistoryPetition : AuditableModelBase<long>
{
    /// <summary>
    /// qaysi kurs formasiga topshirilayotganligi
    /// </summary>
    [Column("petition_for_quran_course_id"),ForeignKey(nameof(PetitionForQuranCourse))] public long PetitionForQuranCourseId { get; set; }
    public virtual PetitionForQuranCourse PetitionForQuranCourse { get; set; }
    /// <summary>
    /// Arizaning oldingi holati
    /// </summary>
    [Column("old_status")] public QuranCoursePetitionStatus OldStatus { get; set; }
    /// <summary>
    /// Arizaning yangi holati
    /// </summary>
    [Column("new_status")] public QuranCoursePetitionStatus NewStatus { get; set; }
}