using System.Linq.Expressions;

namespace SqlQueryBuilder.Abstractions.Parts;

public interface ISqlQueryFrom<T>: ISqlQueryPart where T : ISqlEntity
{
    ISqlQuerySelect<T> SelectAll();
    
    ISqlQuerySelect<T> Select<TS>(Expression<Func<T, TS>> select);

    ISqlQuerySelect<T> Select(
        Expression<Func<T, object?>> select,
        params Expression<Func<T, object?>>[] otherSelect);

    ISqlQuerySelect<TS> Select<TS>(
        Expression<Func<T, TS, object>> select,
        params Expression<Func<T, TS, object>>[] otherSelect);

    ISqlQueryWhere<T> Where(Expression<Func<T, bool>> where);

    ISqlQueryOrder<T> OrderBy(Expression<Func<T, object>> orderBy, params Expression<Func<T, object>>[] otherOrderBy);

    ISqlQueryDelete<T> Delete();
}