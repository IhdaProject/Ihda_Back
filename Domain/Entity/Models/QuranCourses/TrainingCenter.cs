using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;
using Entity.Models.ReferenceBook;

namespace Entity.Models.QuranCourses;
[Table("training_centers", Schema = "quran_courses")]
public class TrainingCenter : ModelBase<long>
{
    [Column("name")]public string Name { get; set; }
    [Column("description")]public string Description { get; set; }
    [Column("address")]public string Address { get; set; }
    [Column("landmark")]public string Landmark { get; set; }
    [Column("phone_number")]public string PhoneNumber { get; set; }
    [Column("photos_link")]public string[] PhotosLink { get; set; }
    [Column("working_hours", TypeName = "jsonb")]public WorkingHour WorkingHours { get; set; }
    [Column("latitude")]public double Latitude { get; set; }
    [Column("longitude")]public double Longitude { get; set; }
    [Column("district_id"),ForeignKey(nameof(District))] public long DistrictId { get; set; }
    public virtual District District { get; set; }
}