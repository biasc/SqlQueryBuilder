using System.Linq.Expressions;

namespace SqlQueryBuilder.Abstractions.Parts;

public interface ISqlQueryFrom<T> :
    ISqlQueryPart,
    ISqlQueryWithSelect<T>
    where T : ISqlEntity
{
    ISqlQuerySelect<T> SelectAll();
    ISqlQueryWhere<T> Where(Expression<Func<T, bool>> where);
    ISqlQueryOrder<T> OrderBy(Expression<Func<T, object>> orderBy, params Expression<Func<T, object>>[] otherOrderBy);
    ISqlQueryDelete<T> Delete();


    ISqlQueryJoin<T, TJ1> InnerJoin<TJ1>(ISqlQuerySelect select, Expression<Func<T, TJ1, bool>> join) where TJ1 : ISqlEntity;
    ISqlQueryJoin<T, TJ1> InnerJoin<TJ1>(Expression<Func<T, TJ1, bool>> join) where TJ1 : ISqlEntity;
    ISqlQueryJoin<T, TJ1> LeftOuterJoin<TJ1>(ISqlQuerySelect<TJ1> select, Expression<Func<T, TJ1, bool>> join) where TJ1 : ISqlEntity;
    ISqlQueryJoin<T, TJ1> LeftOuterJoin<TJ1>(Expression<Func<T, TJ1, bool>> join) where TJ1 : ISqlEntity;
    ISqlQueryJoin<T, TJ1> RightOuterJoin<TJ1>(Expression<Func<T, TJ1, bool>> join) where TJ1 : ISqlEntity;
    ISqlQueryJoin<T, TJ1> RightOuterJoin<TJ1>(ISqlQuerySelect<TJ1> select, Expression<Func<T, TJ1, bool>> join) where TJ1 : ISqlEntity;
}