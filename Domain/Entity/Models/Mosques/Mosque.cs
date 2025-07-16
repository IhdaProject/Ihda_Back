using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models.Mosques;
[Table("mosques",Schema = "prayerful")]
public class Mosque : AuditableModelBase<long>
{
    [Column("name")] public string Name { get; set; }
    [Column("description")] public string Description { get; set; }
    [Column("photo_urls")] public List<string> PhotoUrls { get; set; }
    [Column("latitude")] public double Latitude { get; set; }
    [Column("longitude")] public double Longitude { get; set; }
    public virtual ICollection<MosqueAdmin> MosqueAdmins { get; set; }
}