using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Auth;
using Entity.Models.Common;

namespace Entity.Models.QuranCourses;
[Table("courses", Schema = "quran_courses")]
public class Course : ModelBase<long>
{
    [Column("start_date")]public DateTime StartDate { get; set; }
    [Column("course_form_id"),ForeignKey(nameof(CourseForm))] public long CourseFormId { get; set; }
    public virtual CourseForm CourseForm { get; set; }
    [Column("teacher_id"),ForeignKey(nameof(Teacher))] public long TeacherId { get; set; }
    public virtual User Teacher { get; set; }
}