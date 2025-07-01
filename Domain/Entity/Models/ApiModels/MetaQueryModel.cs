using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Entity.Models.ApiModels;

public class MetaQueryModel
{
    private string? _filteringExpressionsJson;
    private List<MetaQueryFilterModel>? _filteringExpressions;

    public string? FilteringExpressionsJson
    {
        get => _filteringExpressionsJson;
        set
        {
            _filteringExpressionsJson = value;
            _filteringExpressions = string.IsNullOrEmpty(value) 
                ? null 
                : JsonSerializer.Deserialize<List<MetaQueryFilterModel>>(value);
        }
    }

    [JsonIgnore, NotMapped]public List<MetaQueryFilterModel>? FilteringExpressions
    {
        get => _filteringExpressions;
        set
        {
            _filteringExpressions = value;
            _filteringExpressionsJson = value == null 
                ? null 
                : JsonSerializer.Serialize(value);
        }
    }
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 10;
    public List<MetaQuerySortModel>? SortingExpressions { get; set; }
}