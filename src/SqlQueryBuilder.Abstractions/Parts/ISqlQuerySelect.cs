using System.Linq.Expressions;

namespace SqlQueryBuilder.Abstractions.Parts;

public interface ISqlQuerySelect : ISqlQueryPart, ISqlQuery
{
    ISqlQuerySelect Count();
    ISqlQuerySelect Distinct();
}

public interface ISqlQuerySelect<T>: ISqlQuerySelect,ISqlQueryPart, ISqlQuery
{
    // ISqlQuerySelect<T> Count();
    // ISqlQuerySelect<T> Distinct();
//    ISqlQuerySelect<T> SelectAll();
    
    //ISqlQuerySelect<T> Top(int count);
    //ISqlQuerySelect<T> Into();
    //ISqlQuerySelect<T> Insert<TInsert>() where TInsert : ISqlEntity;
}

public interface ISqlQueryWithSelect<T> :ISqlQueryPart
{
    ISqlQuerySelect<TS> Select<TS>(
        Expression<Func<T, TS>> select)  where TS: ISqlEntity;

    ISqlQuerySelect<T> Select(
        Expression<Func<T, object>> select,
        
        params Expression<Func<T, object>>[] otherSelect);
    ISqlQuerySelect<TS> Select<TS>(
        Expression<Func<T, TS, object>> select,
        params Expression<Func<T, TS, object>>[] otherSelect) where TS: ISqlEntity;
}

public interface ISqlQueryWithSelect<T, TJ1> :ISqlQueryPart
    where T : ISqlEntity
    where TJ1 : ISqlEntity
{
    ISqlQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TS>> select) where TS: ISqlEntity;

    ISqlQuerySelect<T> Select(
        Expression<Func<T, object>> select,
        params Expression<Func<T, object>>[] otherSelect);
    
    ISqlQuerySelect<TS> Select<TS>(
        Expression<Func<T, TJ1, TS, object>> select,
        params Expression<Func<T, TJ1, TS, object>>[] otherSelect)  where TS: ISqlEntity;
}

public interface ISqlQueryWithSelect<T, TJ1, TJ2> : 
    ISqlQueryPart
    where T : ISqlEntity
    where TJ1 : ISqlEntity
    where TJ2 : ISqlEntity
{
    ISqlQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TS>> select) where TS : ISqlEntity;

    ISqlQuerySelect<T> Select(
        Expression<Func<T, object>> select,
        params Expression<Func<T, object>>[] otherSelect);
    ISqlQuerySelect<TS> Select<TS>(
        Expression<Func<T, TJ1, TJ2, TS, object>> select,
        params Expression<Func<T, TJ1, TJ2, TS, object>>[] otherSelect) where TS : ISqlEntity;
}

public interface ISqlQueryWithSelect<T, TJ1, TJ2, TJ3> :
    ISqlQueryPart
    where T : ISqlEntity
    where TJ1 : ISqlEntity
    where TJ2 : ISqlEntity
    where TJ3 : ISqlEntity
{
    ISqlQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TS>> select) where TS : ISqlEntity;

    ISqlQuerySelect<T> Select(
        Expression<Func<T, object>> select,
        params Expression<Func<T, object>>[] otherSelect);
    ISqlQuerySelect<TS> Select<TS>(
        Expression<Func<T, TJ1, TJ2, TJ3, TS, object>> select,
        params Expression<Func<T, TJ1, TJ2, TJ3, TS, object>>[] otherSelect) where TS : ISqlEntity;
}

public interface ISqlQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4> : 
    ISqlQueryPart
    where T : ISqlEntity
    where TJ1 : ISqlEntity
    where TJ2 : ISqlEntity
    where TJ3 : ISqlEntity
    where TJ4 : ISqlEntity
{
    ISqlQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TS>> select) where TS : ISqlEntity;

    ISqlQuerySelect<T> Select(
        Expression<Func<T, object>> select,
        params Expression<Func<T, object>>[] otherSelect);
    ISqlQuerySelect<TS> Select<TS>(
        Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TS, object>> select,
        params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TS, object>>[] otherSelect) where TS : ISqlEntity;
}

public interface ISqlQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5> : 
    ISqlQueryPart
    where T : ISqlEntity
    where TJ1 : ISqlEntity
    where TJ2 : ISqlEntity
    where TJ3 : ISqlEntity
    where TJ4 : ISqlEntity
    where TJ5 : ISqlEntity
{
    ISqlQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TS>> select) where TS : ISqlEntity;

    ISqlQuerySelect<T> Select(
        Expression<Func<T, object>> select,
        params Expression<Func<T, object>>[] otherSelect);
    ISqlQuerySelect<TS> Select<TS>(
        Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TS, object>> select,
        params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TS, object>>[] otherSelect) where TS : ISqlEntity;
}

public interface ISqlQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6> : 
    ISqlQueryPart
    where T : ISqlEntity
    where TJ1 : ISqlEntity
    where TJ2 : ISqlEntity
    where TJ3 : ISqlEntity
    where TJ4 : ISqlEntity
    where TJ5 : ISqlEntity
    where TJ6 : ISqlEntity
{
    ISqlQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TS>> select) where TS : ISqlEntity;

    ISqlQuerySelect<T> Select(
        Expression<Func<T, object>> select,
        params Expression<Func<T, object>>[] otherSelect);
    ISqlQuerySelect<TS> Select<TS>(
        Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TS, object>> select,
        params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TS, object>>[] otherSelect) where TS : ISqlEntity;
}

public interface ISqlQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7> : 
    ISqlQueryPart
    where T : ISqlEntity
    where TJ1 : ISqlEntity
    where TJ2 : ISqlEntity
    where TJ3 : ISqlEntity
    where TJ4 : ISqlEntity
    where TJ5 : ISqlEntity
    where TJ6 : ISqlEntity
    where TJ7 : ISqlEntity
{
    ISqlQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TS>> select) where TS : ISqlEntity;

    ISqlQuerySelect<T> Select(
        Expression<Func<T, object>> select,
        params Expression<Func<T, object>>[] otherSelect);
    ISqlQuerySelect<TS> Select<TS>(
        Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TS, object>> select,
        params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TS, object>>[] otherSelect) where TS : ISqlEntity;
}

public interface ISqlQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8> : 
    ISqlQueryPart
    where T : ISqlEntity
    where TJ1 : ISqlEntity
    where TJ2 : ISqlEntity
    where TJ3 : ISqlEntity
    where TJ4 : ISqlEntity
    where TJ5 : ISqlEntity
    where TJ6 : ISqlEntity
    where TJ7 : ISqlEntity
    where TJ8 : ISqlEntity
{
    ISqlQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TS>> select) where TS : ISqlEntity;

    ISqlQuerySelect<T> Select(
        Expression<Func<T, object>> select,
        params Expression<Func<T, object>>[] otherSelect);
    ISqlQuerySelect<TS> Select<TS>(
        Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TS, object>> select,
        params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TS, object>>[] otherSelect) where TS : ISqlEntity;
}

public interface ISqlQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9> : 
    ISqlQueryPart
    where T : ISqlEntity
    where TJ1 : ISqlEntity
    where TJ2 : ISqlEntity
    where TJ3 : ISqlEntity
    where TJ4 : ISqlEntity
    where TJ5 : ISqlEntity
    where TJ6 : ISqlEntity
    where TJ7 : ISqlEntity
    where TJ8 : ISqlEntity
    where TJ9 : ISqlEntity
{
    ISqlQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TS>> select) where TS : ISqlEntity;

    ISqlQuerySelect<T> Select(
        Expression<Func<T, object>> select,
        params Expression<Func<T, object>>[] otherSelect);
    ISqlQuerySelect<TS> Select<TS>(
        Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TS, object>> select,
        params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TS, object>>[] otherSelect) where TS : ISqlEntity;
}

public interface ISqlQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10> : 
    ISqlQueryPart
    where T : ISqlEntity
    where TJ1 : ISqlEntity
    where TJ2 : ISqlEntity
    where TJ3 : ISqlEntity
    where TJ4 : ISqlEntity
    where TJ5 : ISqlEntity
    where TJ6 : ISqlEntity
    where TJ7 : ISqlEntity
    where TJ8 : ISqlEntity
    where TJ9 : ISqlEntity
    where TJ10 : ISqlEntity
{
    ISqlQuerySelect<TS> Select<TS>(
        Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TS>> select) where TS : ISqlEntity;

    ISqlQuerySelect<T> Select(
        Expression<Func<T, object>> select,
        params Expression<Func<T, object>>[] otherSelect);
    ISqlQuerySelect<TS> Select<TS>(
        Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TS, object>> select,
        params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TS, object>>[] otherSelect) where TS : ISqlEntity;
}

