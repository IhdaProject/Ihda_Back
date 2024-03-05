using System.Linq.Expressions;
using System.Reflection;
using Entity.Models.ApiModels;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> Filter<T>(this IQueryable<T> queryable, MetaQueryModel query)
    {
        var filterData = new { PropertyName = query.FilterPropName, Value = query.FilterPropValue };
        if (filterData.PropertyName is null)
            return queryable;
        var property = typeof(T).GetProperties().FirstOrDefault(x => x.Name.Equals(filterData.PropertyName, StringComparison.InvariantCultureIgnoreCase));

        if (property is not PropertyInfo)
            throw new Exception($"{filterData.PropertyName} named property not found");

        var parameter = Expression.Parameter(typeof(T), "x");

        var predicate = Expression
            .Lambda(
                Expression.Equal(
                    Expression.Call(Expression.MakeMemberAccess(parameter, property), "toString", Type.EmptyTypes),
                    Expression.Constant(filterData.Value)),
                parameter
            );

        return queryable
            .Where((Expression<Func<T, bool>>)predicate);
    }

    public static List<String> ExpressionsList = new List<string>() { "==", "!=", "<>", ">>", "<<", "<=", ">=", "$$" };
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
        List<string>? expressions = query.FilteringExpressions;
        if (expressions is null || expressions.DefaultIfEmpty() == null)
            return queryable;
        
        foreach (var expressionStr in expressions)
        {
            var splitted = expressionStr.Split(ExpressionsList.ToArray(), StringSplitOptions.RemoveEmptyEntries);
            var expressionStrType = expressionStr.Substring(splitted[0].Length, 2);

            if (splitted.Length < 2 || !ExpressionsList.Contains(expressionStrType))
                throw new InvalidOperationException();
            var filterData = new { PropertyName = splitted[0], Value = splitted[1] };
            
            var property = typeof(T).GetProperties().FirstOrDefault(x => x.Name.Equals(filterData.PropertyName, StringComparison.InvariantCultureIgnoreCase));
            
            if (property is not PropertyInfo)
                throw new Exception($"{filterData.PropertyName} named property not found");
            
            var parameter = Expression.Parameter(typeof(T), "x");
            
            MethodInfo ilikeMethod = typeof(NpgsqlDbFunctionsExtensions)
                .GetMethod("ILike", new[] { typeof(DbFunctions), typeof(string), typeof(string) })!;

            var efFunctions = Expression.Constant(EF.Functions);
            
            var left = Expression.MakeMemberAccess(parameter, property);
            var right = Expression.Constant(Convert.ChangeType(filterData.Value, property.PropertyType));
            
            Expression switchedExpression = expressionStrType switch
            {
                "!=" => Expression.NotEqual(left, right),
                "<>" => Expression.NotEqual(left, right),
                ">=" => Expression.GreaterThanOrEqual(left, right),
                "<=" => Expression.LessThanOrEqual(left, right),
                "<<" => Expression.LessThan(left, right),
                ">>" => Expression.GreaterThan(left, right),
                "==" => Expression.Equal(left, right),
                "$$" => Expression.Call(ilikeMethod, efFunctions, left, Expression.Constant($"%{filterData.Value}%")),
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
        var sortData = new { PropertyName = query.SortPropName, Direction = query.SortDirection };
        if (sortData.PropertyName is null)
            return queryable;
        var property = typeof(T).GetProperties().FirstOrDefault(x => x.Name.Equals(sortData.PropertyName, StringComparison.InvariantCultureIgnoreCase));

        if (property is not PropertyInfo)
            throw new Exception($"{sortData.PropertyName} named property not found");

        var parameter = Expression.Parameter(typeof(T), "x");

        var lambda = (Expression<Func<T, object>>)Expression
            .Lambda(
                    Expression.Convert(Expression.MakeMemberAccess(parameter, property), typeof(object)),
                parameter
            );
        
        return queryable.Provider
            .CreateQuery<T>(Expression.Call(
                typeof(Queryable), 
                sortData.Direction == "asc" ? "OrderBy" : "OrderByDescending", 
                new Type[]{queryable.ElementType, typeof(object)}, 
                queryable.Expression, lambda));
    }

    public static IQueryable<T> Paging<T>(this IQueryable<T> queryable, MetaQueryModel query)
    {
        var pagingData = new { query.Skip, query.Take };
        return queryable
            .Skip(pagingData.Skip)
            .Take(pagingData.Take);
    }
}