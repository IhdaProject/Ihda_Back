using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models.ReferenceBook;

[Table("type_change_histories", Schema = "reference_book")]
public class TypeChangeHistory : AuditableModelBase<long>
{
    [Column("commit")] public string Commit { get; set; }
    [Column("fields")] public List<FieldSchema> Fields { get; set; }
}