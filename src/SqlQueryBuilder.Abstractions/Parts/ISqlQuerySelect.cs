using System.Linq.Expressions;

namespace SqlQueryBuilder.Abstractions.Parts;

public interface ISqlQuerySelect<T>: ISqlQueryPart, ISqlQuery
{
    ISqlQuerySelect<T> Count();
    ISqlQuerySelect<T> Distinct();
    //ISqlQuerySelect<T> Top(int count);
    //ISqlQuerySelect<T> Into();
    //ISqlQuerySelect<T> Insert<TInsert>() where TInsert : ISqlEntity;
}

public interface ISqlQueryWithSelect<T> 
{
    ISqlQuerySelect<T> SelectAll();
}

public interface ISqlQueryWithSelect<T, TJ1> : ISqlQuerySelect<T>
    where T : ISqlEntity
    where TJ1 : ISqlEntity
{
    ISqlQuerySelect<TS> Select<TS>(
        Expression<Func<T, TJ1, TS>> select);

    ISqlQuerySelect<TS> Select<TS>(
        Expression<Func<T, TJ1, TS, object>> select,
        params Expression<Func<T, TJ1, TS, object>>[] otherSelect);
}
