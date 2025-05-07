using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Auth;
using Entity.Models.Common;

namespace Entity.Models.QuranCourses;
[Table("course_form_teachers", Schema = "quran_courses")]
public class CourseFormTeacher : ModelBase<long>
{
    [Column("course_form_id"),ForeignKey(nameof(CourseForm))] public long CourseFormId { get; set; }
    public virtual CourseForm CourseForm { get; set; }
    [Column("user_id"),ForeignKey(nameof(User))] public long userId { get; set; }
    public virtual User User { get; set; }
}