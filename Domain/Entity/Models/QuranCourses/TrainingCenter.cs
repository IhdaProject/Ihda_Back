using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;
using Entity.Models.ReferenceBook;

namespace Entity.Models.QuranCourses;
/// <summary>
/// O'quv markaz
/// </summary>
[Table("training_centers", Schema = "quran_courses")]
public class TrainingCenter : AuditableModelBase<long>
{
    /// <summary>
    /// O'quv markaz nomi
    /// </summary>
    [Column("name")]public string Name { get; set; }
    /// <summary>
    /// O'quv markaz haida qisqacha ma'lumot
    /// </summary>
    [Column("description")]public string Description { get; set; }
    /// <summary>
    /// O'quv markaz manzili
    /// </summary>
    [Column("address")]public string Address { get; set; }
    /// <summary>
    /// O'quv markaz manzili uchun mo'jal
    /// </summary>
    [Column("landmark")]public string Landmark { get; set; }
    /// <summary>
    /// O'quv markaz rasmiy raqami
    /// </summary>
    [Column("phone_number")]public string PhoneNumber { get; set; }
    /// <summary>
    /// O'quv markaz rasmlari
    /// </summary>
    [Column("photos_link", TypeName = "text[]")]public string[]? PhotosLink { get; set; }
    /// <summary>
    /// Ish kunnlari va soatlari
    /// </summary>
    [Column("working_hours", TypeName = "jsonb")]public WorkingHour WorkingHours { get; set; }
    /// <summary>
    /// Kenglik
    /// </summary>
    [Column("latitude")]public double Latitude { get; set; }
    /// <summary>
    /// Kenglik
    /// </summary>
    [Column("longitude")]public double Longitude { get; set; }
    /// <summary>
    /// Joylashgan hududini belgilaydi
    /// </summary>
    [Column("district_id"),ForeignKey(nameof(District))] public long DistrictId { get; set; }
    public virtual District District { get; set; }
    public virtual ICollection<CenterManager> CenterManagers { get; set; }
    public virtual ICollection<CourseForm> CourseForms { get; set; }
}