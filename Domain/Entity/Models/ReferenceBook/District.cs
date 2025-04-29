using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models.ReferenceBook;

[Table("districts", Schema = "reference_book"), Index(nameof(Code), IsUnique = true)]
public class District : ModelBase<long>
{
    [Column("name")] public string Name { get; set; }
    [Column("code")] public int Code { get; set; }
    [Column("region_id"),ForeignKey(nameof(Region))] public long RegionId { get; set; }
    public virtual Region Region { get; set; }
}