using System.Linq.Expressions;

namespace SqlQueryBuilder.Abstractions.Parts;

public interface ISqlQueryWhere<T> 
    : ISqlQueryPart, ISqlQuery
    where T: ISqlEntity
{
    Expression? GetWhere();
    
    ISqlQueryDelete<T> Delete();
    
    ISqlQuerySelect<T> Select<TS>(
        Expression<Func<T, TS>> select);

    ISqlQuerySelect<T> Select(
        Expression<Func<T, object?>> select,
        params Expression<Func<T, object?>>[] otherSelect);
}