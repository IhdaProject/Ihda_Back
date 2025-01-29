namespace Entity.Models.ApiModels;

public abstract class MetaQueryFilterModel
{
    public string PropertyName { get; set; }
    public string Value { get; set; }
    public string Type { get; set; }
}