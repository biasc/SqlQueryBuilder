using System.Linq.Expressions;

namespace SqlQueryBuilder.Abstractions.Parts;

public interface ISqlQueryOrder<T> :
    ISqlQueryWithSelect<T>,
    ISqlQueryPart
    where T : ISqlEntity
{
}

public interface ISqlQueryOrder<T, TJ1>:
    ISqlQueryWithSelect<T, TJ1>,
    ISqlQueryPart
    where T : ISqlEntity
    where TJ1 : ISqlEntity
{
    ISqlQueryOrder<T, TJ1> OrderBy(
        Expression<Func<T, TJ1, object?>> orderBy,
        params Expression<Func<T, TJ1, object?>>[] otherOrderBy);
}

public interface ISqlQueryOrder<T, TJ1, TJ2>:
    ISqlQueryWithSelect<T, TJ1, TJ2>,
    ISqlQueryPart
    where T : ISqlEntity
    where TJ1 : ISqlEntity
    where TJ2 : ISqlEntity
{
    ISqlQueryOrder<T, TJ1, TJ2> OrderBy(
        Expression<Func<T, TJ1, TJ2, object?>> orderBy,
        params Expression<Func<T, TJ1, TJ2, object?>>[] otherOrderBy);
}

public interface ISqlQueryOrder<T, TJ1, TJ2, TJ3>:
    ISqlQueryWithSelect<T, TJ1, TJ2, TJ3>,
    ISqlQueryPart
        where T : ISqlEntity
        where TJ1 : ISqlEntity
        where TJ2 : ISqlEntity
        where TJ3 : ISqlEntity
    {
        ISqlQueryOrder<T, TJ1, TJ2, TJ3> OrderBy(
            Expression<Func<T, TJ1, TJ2, TJ3, object?>> orderBy,
            params Expression<Func<T, TJ1, TJ2, TJ3, object?>>[] otherOrderBy);
    }

public interface ISqlQueryOrder<T, TJ1, TJ2, TJ3, TJ4>:
    ISqlQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4>,
    ISqlQueryPart
        where T : ISqlEntity
        where TJ1 : ISqlEntity
        where TJ2 : ISqlEntity
        where TJ3 : ISqlEntity
        where TJ4 : ISqlEntity
    {
        ISqlQueryOrder<T, TJ1, TJ2, TJ3, TJ4> OrderBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, object?>> orderBy,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, object?>>[] otherOrderBy);
    }

public interface ISqlQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5>:
    ISqlQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5>,
    ISqlQueryPart
        where T : ISqlEntity
        where TJ1 : ISqlEntity
        where TJ2 : ISqlEntity
        where TJ3 : ISqlEntity
        where TJ4 : ISqlEntity
        where TJ5 : ISqlEntity
    {
        ISqlQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5> OrderBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, object?>> orderBy,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, object?>>[] otherOrderBy);
    }

public interface ISqlQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6>:
    ISqlQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6>,
    ISqlQueryPart
        where T : ISqlEntity
        where TJ1 : ISqlEntity
        where TJ2 : ISqlEntity
        where TJ3 : ISqlEntity
        where TJ4 : ISqlEntity
        where TJ5 : ISqlEntity
        where TJ6 : ISqlEntity
    {
        ISqlQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6> OrderBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, object?>> orderBy,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, object?>>[] otherOrderBy);
    }

public interface ISqlQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7>:
    ISqlQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7>,
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
        ISqlQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7> OrderBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, object?>> orderBy,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, object?>>[] otherOrderBy);
    }

public interface ISqlQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8>:
        ISqlQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8>,
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
        ISqlQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8> OrderBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, object?>> orderBy,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, object?>>[] otherOrderBy);
    }

public interface ISqlQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9>:
        ISqlQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9>,
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
        ISqlQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9> OrderBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, object?>> orderBy,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, object?>>[] otherOrderBy);
    }

public interface ISqlQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10>:
        ISqlQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ10>,
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
        ISqlQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10> OrderBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, object?>> orderBy,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, object?>>[] otherOrderBy);
    }
