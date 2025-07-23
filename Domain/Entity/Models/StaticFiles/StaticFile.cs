using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models.StaticFiles;
[Table("static_files",Schema = "asset")]
public class StaticFile : AuditableModelBase<long>
{
    [Column("path")] public string Path { get; set; }
    [Column("url")] public string Url { get; set; }
    [Column("size")] public long? Size { get; set; }
    [Column("type")] public string? Type { get; set; }
    [Column("file_extension")] public string? FileExtension { get; set; }
    [Column("old_name")] public string? OldName { get; set; }
}