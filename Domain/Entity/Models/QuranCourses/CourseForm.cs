using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models.QuranCourses;
[Table("course_forms", Schema = "quran_courses")]
public class CourseForm : ModelBase<long>
{
    [Column("name")]public string Name { get; set; }
    [Column("description")]public string Description { get; set; }
    [Column("admission_quota")]public int AdmissionQuota { get; set; }
    [Column("duration")]public int Duration { get; set; }
    [Column("working_hours", TypeName = "jsonb")]public WorkingHour TransitionTime { get; set; }
    [Column("training_center_id"),ForeignKey(nameof(TrainingCenter))] public long TrainingCenterId { get; set; }
    public virtual TrainingCenter TrainingCenter { get; set; }
    public virtual ICollection<CourseFormTeacher> Teachers { get; set; }
}