using System.ComponentModel.DataAnnotations.Schema;
using Entity.Enums;
using Entity.Models.Auth;
using Entity.Models.Common;

namespace Entity.Models.QuranCourses;
/// <summary>
/// O'quv kursi
/// </summary>
[Table("courses", Schema = "quran_courses")]
public class Course : AuditableModelBase<long>
{
    /// <summary>
    /// Kursning holati
    /// </summary>
    public CourseStatus Status { get; set; }
    /// <summary>
    /// Kursning boshlangan sanasi
    /// </summary>
    [Column("start_date")]public DateTime StartDate { get; set; }
    /// <summary>
    /// Kurs formasini belgilaydi
    /// </summary>
    [Column("course_form_id"),ForeignKey(nameof(CourseForm))] public long CourseFormId { get; set; }
    public virtual CourseForm CourseForm { get; set; }
    /// <summary>
    /// Kurs o'qituvchisini belgilaydi
    /// </summary>
    [Column("teacher_id"),ForeignKey(nameof(Teacher))] public long TeacherId { get; set; }
    public virtual CourseFormTeacher Teacher { get; set; }
    
}