namespace Entity.Models.ApiModels;

public class MetaQuerySortModel
{
    public string SortFieldName { get; set; }
    public string Direction { get; set; } = "asc";
}