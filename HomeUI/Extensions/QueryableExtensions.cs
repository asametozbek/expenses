using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HomeUI.Extensions
{
    public static partial class QueryableExtensions
    {
        public static IQueryable<T> WhereAnyMatch<T, V>(this IQueryable<T> source, IEnumerable<V> values, Expression<Func<T, V, bool>> match)
        {
            var parameter = match.Parameters[0];
            var body = values
                // the easiest way to let EF Core use parameter in the SQL query rather than literal value
                .Select(value => ((Expression<Func<V>>)(() => value)).Body)
                .Select(value => Expression.Invoke(match, parameter, value))
                .Aggregate<Expression>(Expression.OrElse);
            var predicate = Expression.Lambda<Func<T, bool>>(body, parameter);
            return source.Where(predicate);
        }
    }
}
