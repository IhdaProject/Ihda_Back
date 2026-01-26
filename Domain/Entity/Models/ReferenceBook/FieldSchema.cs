using Entity.Enums;

namespace Entity.Models.ReferenceBook;

public class FieldSchema
{
    /// <summary>
    /// Field name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Field type
    /// <example>Dynamic Type, char, string, int, double, bool, DateTime</example>
    /// </summary>
    public TypeForDynamic Type { get; set; }
    /// <summary>
    /// Reference Type Id
    /// </summary>
    public int? TypeId { get; set; } = null;
    /// <summary>
    /// Is Required
    /// </summary>
    public bool Required { get; set; } = false;
    /// <summary>
    /// Is Unique
    /// </summary>
    public bool Unique { get; set; } = false;
    /// <summary>
    /// Is Array
    /// </summary>
    public bool IsArray { get; set; } = false;
    /// <summary>
    /// Item length or value. minimum value
    /// </summary>
    public int? Min { get; set; }
    /// <summary>
    /// Item length or value. maximum value
    /// </summary>
    public int? Max { get; set; }
    /// <summary>
    /// Array min length
    /// </summary>
    public int? MinLength { get; set; }
    /// <summary>
    /// Array max length
    /// </summary>
    public int? MaxLength { get; set; }
    /// <summary>
    /// Regex pattern for string value.
    /// </summary>
    public string? Pattern { get; set; }
}