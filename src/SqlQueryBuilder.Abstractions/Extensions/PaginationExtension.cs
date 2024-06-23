using System.Linq.Expressions;
using SqlQueryBuilder.Abstractions.Parts;
using SqlQueryBuilder.Abstractions.Statements;

namespace SqlQueryBuilder.Abstractions.Extensions;

public static class PaginationExtension
{
    public static ISqlQueryPage Offset(this ISqlQueryPage obj, int offset)
    {
        return default!;
    }

    public static object Fetch(this ISqlQueryPage obj, int fetch)
    {
        return default!;
    }

    public static int Over(this int obj, Expression<Func<IRowNumberOver, object>> expression)
    {
        return default;
    }

    public static long Over(this long obj, Expression<Func<IRowNumberOver, object>> expression)
    {
        return default;
    }

    public static IRowNumberOver PartitionBy(this IRowNumberOver over, params Expression<Func<object>>[] partitionBy)
    {
        return default!;
    }

    public static IRowNumberOver OrderBy(this IRowNumberOver over, params Expression<Func<object>>[] orderBy)
    {
        return default!;
    }
}