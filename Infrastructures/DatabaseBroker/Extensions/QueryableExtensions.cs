using System.Linq.Expressions;
using System.Reflection;
using Entity.Models.ApiModels;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Extensions;

public static class QueryableExtensions
{
    private static List<string> ExpressionsList = ["==", "!=", "<>", ">>", "<<", "<=", ">=", "$$"];
    /// <summary>
    /// Filter by expressions
    /// </summary>
    /// <param name="queryable"></param>
    /// <param name="expressions">an expression is [PropertyName][==, !=, <>, >>, <<, <=, >=]</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static IQueryable<T> FilterByExpressions<T>(this IQueryable<T> queryable, MetaQueryModel query)
    {
        if (query.FilteringExpressions is null)
            return queryable;
        
        foreach (var expression in query.FilteringExpressions)
        {
            var filterData = new { expression.PropertyName, Value = expression.Value };
            
            var property = typeof(T).GetProperties().FirstOrDefault(x => x.Name.Equals(filterData.PropertyName, StringComparison.InvariantCultureIgnoreCase));
            
            if (property is null)
                throw new Exception($"{filterData.PropertyName} named property not found");
            
            var parameter = Expression.Parameter(typeof(T), "x");
            
            var iLikeMethod = typeof(NpgsqlDbFunctionsExtensions)
                .GetMethod("ILike", [typeof(DbFunctions), typeof(string), typeof(string)])!;

            var efFunctions = Expression.Constant(EF.Functions);
            
            var left = Expression.MakeMemberAccess(parameter, property);
            var right = Expression.Constant(Convert.ChangeType(filterData.Value, property.PropertyType));
            
            Expression switchedExpression = expression.Type switch
            {
                "!=" => Expression.NotEqual(left, right),
                "<>" => Expression.NotEqual(left, right),
                ">=" => Expression.GreaterThanOrEqual(left, right),
                "<=" => Expression.LessThanOrEqual(left, right),
                "<<" => Expression.LessThan(left, right),
                ">>" => Expression.GreaterThan(left, right),
                "$$" => Expression.Call(iLikeMethod, efFunctions, left, Expression.Constant($"%{filterData.Value}%")),
                "==" => Expression.Equal(left, right),
                _ => Expression.Equal(left, right)
            };
            
            var predicate = Expression
                .Lambda(
                    switchedExpression,
                    parameter
                );

           queryable = queryable
                .Where((Expression<Func<T, bool>>)predicate);
        }

        return queryable;
    }
    
    public static IQueryable<T> Sort<T>(this IQueryable<T> queryable, MetaQueryModel query)
    {
        if (query.SortingExpressions is null)
            return queryable;
        
        var one = true;
        foreach (var sortData in query.SortingExpressions?.Where(sort => !string.IsNullOrEmpty(sort.SortFieldName)).Select(sort => new { PropertyName = sort.SortFieldName, sort.Direction })!)
        {
            var property = typeof(T).GetProperties()
                .FirstOrDefault(x => x.Name.Equals(
                    sortData.PropertyName,
                    StringComparison.InvariantCultureIgnoreCase));

            if (property is null)
                throw new Exception($"{sortData.PropertyName} named property not found");

            var parameter = Expression.Parameter(typeof(T), "x");

            var lambda = (Expression<Func<T, object>>)Expression
                .Lambda(
                    Expression.Convert(Expression.MakeMemberAccess(parameter, property), typeof(object)),
                    parameter);

            string method;
            if (one)
            {
                one = false;
                method = sortData.Direction == "asc" ? "OrderBy" : "OrderByDescending";
            }
            else
                method = sortData.Direction == "asc" ? "ThenBy" : "ThenByDescending";
            
            queryable = queryable.Provider
                .CreateQuery<T>(Expression.Call(
                    typeof(Queryable), 
                    method,
                    [queryable.ElementType, typeof(object)], 
                    queryable.Expression, lambda));
        }

        return queryable;
    }

    public static IQueryable<T> Paging<T>(this IQueryable<T> queryable, MetaQueryModel query)
    {
        var pagingData = new { query.Skip, query.Take };
        return queryable
            .Skip(pagingData.Skip)
            .Take(pagingData.Take);
    }
}