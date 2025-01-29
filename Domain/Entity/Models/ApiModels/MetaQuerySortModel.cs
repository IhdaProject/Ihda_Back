namespace Entity.Models.ApiModels;

public abstract class MetaQuerySortModel
{
    public string PropertyName { get; set; }
    public string Direction { get; set; } = "asc";
}