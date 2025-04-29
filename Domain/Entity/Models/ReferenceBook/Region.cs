using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models.ReferenceBook;
[Table("regions", Schema = "reference_book"), Index(nameof(Code), IsUnique = true)]
public class Region : ModelBase<long>
{
    [Column("name")] public string Name { get; set; }
    [Column("code")] public int Code { get; set; }
    [Column("country_id"),ForeignKey(nameof(Country))] public long CountryId { get; set; }
    public virtual Country Country { get; set; }
}