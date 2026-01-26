using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Auth;
using Entity.Models.Common;

namespace Entity.Models.QuranCourses;
/// <summary>
/// Kurs formasiga biriktirilgan o'qituvchi
/// </summary>
[Table("course_form_teachers", Schema = "quran_courses")]
public class CourseFormTeacher : AuditableModelBase<long>
{
    /// <summary>
    /// qaysi kurs formasiga bog'langanligi
    /// </summary>
    [Column("course_form_id"),ForeignKey(nameof(CourseForm))] public long CourseFormId { get; set; }
    public virtual CourseForm CourseForm { get; set; }
    /// <summary>
    /// aynan qaysi foydalanuvchiligi
    /// </summary>
    [Column("user_id"),ForeignKey(nameof(User))] public long UserId { get; set; }
    public virtual User User { get; set; }
    /// <summary>
    /// Shu o'qituvchi qaysi kurslarda dars bergani va berayotgani
    /// </summary>
    public virtual ICollection<Course> Courses { get; set; }
}