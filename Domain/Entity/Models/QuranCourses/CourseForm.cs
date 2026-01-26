using System.ComponentModel.DataAnnotations.Schema;
using Entity.Enums;
using Entity.Models.Common;

namespace Entity.Models.QuranCourses;
/// <summary>
/// O'quv markazdagi kurslar formasi(qolibi)
/// </summary>
[Table("course_forms", Schema = "quran_courses")]
public class CourseForm : AuditableModelBase<long>
{
    /// <summary>
    /// Forma nomi
    /// </summary>
    [Column("name")]public string Name { get; set; }
    /// <summary>
    /// Forma haqida qisqacha ma'lumot
    /// </summary>
    [Column("description")]public string Description { get; set; }
    /// <summary>
    /// Formadan hosil bo'ladigan kursning o'quvchi sig'imi
    /// </summary>
    [Column("admission_quota")]public int AdmissionQuota { get; set; }
    /// <summary>
    /// kursning davomiyligi(kunlarda)
    /// </summary>
    [Column("duration")]public int Duration { get; set; }
    /// <summary>
    /// Eng kichik yosh chegarasi
    /// </summary>
    [Column("min_years_old")]public int? MinYearsOld { get; set; }
    /// <summary>
    /// Eng katta yosh chegarasi
    /// </summary>
    [Column("max_years_old")]public int? MaxYearsOld { get; set; }
    /// <summary>
    /// Muayyan jinsdagilar o'qishiga mo'jallangan bo'lsa
    /// </summary>
    [Column("for_gender")]public Gender? ForGender { get; set; }
    /// <summary>
    /// qaysi kunlar va qaysi paytlarda o'qitilishi
    /// </summary>
    [Column("working_hours", TypeName = "jsonb")]public WorkingHour? TransitionTime { get; set; }
    /// <summary>
    /// Aynan qaysi o'quv markazga tegishliligi
    /// </summary>
    [Column("training_center_id"),ForeignKey(nameof(TrainingCenter))] public long TrainingCenterId { get; set; }
    public virtual TrainingCenter TrainingCenter { get; set; }
    public virtual ICollection<CourseFormTeacher> Teachers { get; set; }
}