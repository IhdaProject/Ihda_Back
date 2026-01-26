using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models.ReferenceBook;

[Table("type_schemas", Schema = "reference_book"), Index(nameof(Name), IsUnique = true)]
public class TypeSchema : AuditableModelBase<long>
{
    [Column("name")] public string Name { get; set; }
    [Column("description")] public string Description { get; set; }
    [Column("fields", TypeName = "jsonb")] public List<FieldSchema> Fields { get; set; }
}