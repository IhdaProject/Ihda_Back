namespace Entity.Models.ApiModels;

public abstract class MetaQueryModel
{
    public List<MetaQueryFilterModel>? FilteringExpressions { get; set; }
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 10;
    public List<MetaQuerySortModel>? SortingExpressions { get; set; }
}