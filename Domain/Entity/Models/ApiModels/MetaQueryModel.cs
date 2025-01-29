namespace Entity.Models.ApiModels;

public class MetaQueryModel
{
    public List<MetaQueryFilterModel>? FilteringExpressions { get; set; }
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 10;
    public List<MetaQuerySortModel>? SortingExpressions { get; set; }
}