using System.Collections;
using System.Linq.Expressions;
using SqlQueryBuilder.Abstractions;
using SqlQueryBuilder.Abstractions.Extensions;
using SqlQueryBuilder.Abstractions.Parts;

namespace SqlQueryBuilder.Parts;

public abstract class SqlQueryOrder: ISqlQueryPart
{
    protected readonly IList<ISqlQueryPart> SqlParts;
    protected readonly Expression[] OrderBy;
    protected SqlQueryOrder(IList<ISqlQueryPart>? sqlParts, Expression orderBy, Expression[] otherOrderBy)
    {
        SqlParts = sqlParts ?? new List<ISqlQueryPart>();
        OrderBy = orderBy.Merge(otherOrderBy);

        SqlParts.Add(this);
    }

    public SqlPartType PartType => SqlPartType.Order;
    public string Compute(ISqlQueryTranslator translator)
    {
        return $"ORDER BY {string.Join(", ", OrderBy.Select(o => translator.Compute(o)))}";
    }
}

public class SqlQueryOrder<T> : SqlQueryOrder, ISqlQueryOrder<T>
    where T : ISqlEntity
{
    internal SqlQueryOrder(
        IList<ISqlQueryPart>? sqlParts,
        Expression orderBy,
        Expression[] otherOrderBy)
        : base(sqlParts, orderBy, otherOrderBy)
    {
    }
    public ISqlQuerySelect<TS> Select<TS>(Expression<Func<T, TS>> select) where TS: ISqlEntity
    {
        return new SqlQuerySelect<TS>(SqlParts, [select]);
    }

    public ISqlQuerySelect<T> Select(Expression<Func<T, object>> select, params Expression<Func<T, object>>[] otherSelect)
    {
        return new SqlQuerySelect<T>(SqlParts, [select]);
    }

    public ISqlQuerySelect<TS> Select<TS>(Expression<Func<T, TS, object>> select, params Expression<Func<T, TS, object>>[] otherSelect) where TS : ISqlEntity
    {
        return new SqlQuerySelect<TS>(SqlParts, [select]);
    }
}

public class SqlQueryOrder<T, TJ1> : SqlQueryOrder, ISqlQueryOrder<T, TJ1>
    where T : ISqlEntity
    where TJ1 : ISqlEntity
{
    internal SqlQueryOrder(
        IList<ISqlQueryPart>? sqlParts,
        Expression orderBy,
        Expression[] otherOrderBy) 
        : base(sqlParts, orderBy, otherOrderBy) { }

    public ISqlQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TS>> select) where TS : ISqlEntity
    {
        return new SqlQuerySelect<TS>(SqlParts, select.Merge());
    }

    public ISqlQuerySelect<T> Select(Expression<Func<T, object>> select, params Expression<Func<T, object>>[] otherSelect)
    {
        return new SqlQuerySelect<T>(SqlParts, select.Merge(otherSelect));
    }

    public ISqlQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TS, object>> select, params Expression<Func<T, TJ1, TS, object>>[] otherSelect) where TS : ISqlEntity
    {
        return new SqlQuerySelect<TS>(SqlParts, select.Merge(otherSelect));
    }

    public ISqlQueryOrder<T, TJ1> OrderBy(
        Expression<Func<T, TJ1, object?>> orderBy, params Expression<Func<T, TJ1, object?>>[] otherOrderBy)
    {
        return new SqlQueryOrder<T, TJ1>(SqlParts, orderBy, otherOrderBy);
    }
}

public class SqlQueryOrder<T, TJ1, TJ2> : SqlQueryOrder, ISqlQueryOrder<T, TJ1, TJ2>
    where T : ISqlEntity
    where TJ1 : ISqlEntity
    where TJ2 : ISqlEntity
{
    internal SqlQueryOrder(
        IList<ISqlQueryPart>? sqlParts,
        Expression orderBy,
        Expression[] otherOrderBy) 
        : base(sqlParts, orderBy, otherOrderBy) { }

    public ISqlQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TS>> select) where TS : ISqlEntity
    {
        return new SqlQuerySelect<TS>(SqlParts, select.Merge());
    }

    public ISqlQuerySelect<T> Select(Expression<Func<T, object>> select, params Expression<Func<T, object>>[] otherSelect)
    {
        return new SqlQuerySelect<T>(SqlParts, select.Merge(otherSelect));
    }

    public ISqlQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TS, object>> select, params Expression<Func<T, TJ1, TJ2, TS, object>>[] otherSelect) where TS : ISqlEntity
    {
        return new SqlQuerySelect<TS>(SqlParts, select.Merge(otherSelect));
    }

    public ISqlQueryOrder<T, TJ1, TJ2> OrderBy(Expression<Func<T, TJ1, TJ2, object?>> orderBy, params Expression<Func<T, TJ1, TJ2, object?>>[] otherOrderBy)
    {
        return new SqlQueryOrder<T, TJ1, TJ2>(SqlParts, orderBy, otherOrderBy);
    }
}