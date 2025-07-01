using System.ComponentModel.DataAnnotations.Schema;
using Entity.Enums;
using Entity.Models.Auth;
using Entity.Models.Common;

namespace Entity.Models.QuranCourses;
[Table("petition_for_quran_course", Schema = "quran_courses")]
public class PetitionForQuranCourse : ModelBase<long>
{
    [Column("course_form_id"),ForeignKey(nameof(CourseForm))] public long CourseFormId { get; set; }
    public virtual CourseForm CourseForm { get; set; }
    [Column("user_id"),ForeignKey(nameof(User))] public long? userId { get; set; }
    public virtual User? User { get; set; }
    [Column("pinfl")]public string Pinfl { get; set; }
    [Column("passport")]public string Passport { get; set; }
    [Column("birthday")]public DateTime BirthDay { get; set; }
    [Column("full_name")]public string FullName { get; set; }
    [Column("duration")]public Gender Gender { get; set; }
}