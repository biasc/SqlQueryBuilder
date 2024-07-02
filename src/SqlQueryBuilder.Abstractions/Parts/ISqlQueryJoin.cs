using System.Linq.Expressions;

namespace SqlQueryBuilder.Abstractions.Parts;

public enum JoinType
{
    Inner,
    Left,
    Right,
}

public interface ISqlQueryJoin : ISqlQueryPart
{
    
}

public interface ISqlQueryJoin<T, TJ1> : 
    ISqlQueryJoin,
    ISqlQueryWithSelect<T,TJ1>
    where T : ISqlEntity
    where TJ1 : ISqlEntity
{
    ISqlQueryJoin<T, TJ1> InnerJoin<TJ2>(Expression<Func<T, TJ1, TJ2, bool>> join) 
        where TJ2 : ISqlEntity;
    
    ISqlQueryJoin<T, TJ1, TJ2> InnerJoin<TJ2>(
        ISqlQuerySelect<TJ2> select, Expression<Func<T, TJ1, TJ2, bool>> join)
        where TJ2 : ISqlEntity;
    
    ISqlQueryJoin<T, TJ1> LeftJoin<TJ2>(Expression<Func<T, TJ1, TJ2, bool>> join) 
        where TJ2 : ISqlEntity;
    
    ISqlQueryJoin<T, TJ1, TJ2> LeftJoin<TJ2>(
        ISqlQuerySelect<TJ2> select, Expression<Func<T, TJ1, TJ2, bool>> join)
        where TJ2 : ISqlEntity;
    
    ISqlQueryJoin<T, TJ1> RightJoin<TJ2>(Expression<Func<T, TJ1, TJ2, bool>> join)
        where TJ2 : ISqlEntity;
    
    ISqlQueryJoin<T, TJ1, TJ2> RightJoin<TJ2>(
        ISqlQuerySelect<TJ2> select, Expression<Func<T, TJ1, TJ2, bool>> join)
        where TJ2 : ISqlEntity;

    ISqlQueryOrder<T, TJ1> OrderBy(Expression<Func<T, TJ1, object>> orderBy,
        params Expression<Func<T, TJ1, object>>[] otherOrderBy);
}

public interface ISqlQueryJoin<T, TJ1, TJ2> : ISqlQueryJoin
    where T : ISqlEntity
    where TJ1 : ISqlEntity
    where TJ2 : ISqlEntity
{
    ISqlQueryJoin<T, TJ1, TJ2> InnerJoin<TJ3>(Expression<Func<T, TJ1, TJ2, TJ3, bool>> join) 
        where TJ3 : ISqlEntity;
    
    ISqlQueryJoin<T, TJ1, TJ2> InnerJoin<TJ3>(
        ISqlQuerySelect<TJ2> select, Expression<Func<T, TJ1, TJ2, TJ3, bool>> join)
        where TJ3 : ISqlEntity;
    
    ISqlQueryJoin<T, TJ1, TJ2> LeftJoin<TJ3>(Expression<Func<T, TJ1, TJ2, TJ3, bool>> join) 
        where TJ3 : ISqlEntity;
    
    ISqlQueryJoin<T, TJ1, TJ2> LeftJoin<TJ3>(
        ISqlQuerySelect<TJ2> select, Expression<Func<T, TJ1, TJ2, TJ3, bool>> join)
        where TJ3 : ISqlEntity;
    
    ISqlQueryJoin<T, TJ1, TJ2> RightJoin<TJ3>(Expression<Func<T, TJ1, TJ2, TJ3, bool>> join)
        where TJ3 : ISqlEntity;

    ISqlQueryJoin<T, TJ1, TJ2> RightJoin<TJ3>(
        ISqlQuerySelect<TJ2> select, Expression<Func<T, TJ1, TJ2, TJ3, bool>> join)
        where TJ3 : ISqlEntity;

    ISqlQueryOrder<T, TJ1, TJ2> OrderBy(Expression<Func<T, TJ1, TJ2, object>> orderBy, 
        params Expression<Func<T, TJ1, TJ2, object>>[] otherOrderBy);
}

public interface ISqlQueryJoin<T, TJ1, TJ2, TJ3> : ISqlQueryJoin
    where T : ISqlEntity
    where TJ1 : ISqlEntity
    where TJ2 : ISqlEntity
    where TJ3 : ISqlEntity
{
    ISqlQueryJoin<T, TJ1, TJ2, TJ3> InnerJoin<TJ4>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, bool>> join) 
        where TJ4 : ISqlEntity;
    
    ISqlQueryJoin<T, TJ1, TJ2, TJ3> InnerJoin<TJ4>(
        ISqlQuerySelect<TJ3> select, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, bool>> join)
        where TJ4 : ISqlEntity;
    
    ISqlQueryJoin<T, TJ1, TJ2, TJ3> LeftJoin<TJ4>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, bool>> join) 
        where TJ4 : ISqlEntity;
    
    ISqlQueryJoin<T, TJ1, TJ2, TJ3> LeftJoin<TJ4>(
        ISqlQuerySelect<TJ3> select, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, bool>> join)
        where TJ4 : ISqlEntity;
    
    ISqlQueryJoin<T, TJ1, TJ2, TJ3> RightJoin<TJ4>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, bool>> join)
        where TJ4 : ISqlEntity;

    ISqlQueryJoin<T, TJ1, TJ2, TJ3> RightJoin<TJ4>(
        ISqlQuerySelect<TJ3> select, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, bool>> join)
        where TJ4 : ISqlEntity;

    ISqlQueryOrder<T, TJ1, TJ2, TJ3> OrderBy(Expression<Func<T, TJ1, TJ2, TJ3, object>> orderBy, 
        params Expression<Func<T, TJ1, TJ2, TJ3, object>>[] otherOrderBy);
}

#warning continuare con interfacce fino a 10