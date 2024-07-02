using System.Linq.Expressions;
using System.Text;
using SqlQueryBuilder.Abstractions;
using SqlQueryBuilder.Abstractions.Extensions;
using SqlQueryBuilder.Abstractions.Parts;

namespace SqlQueryBuilder.Parts;

public abstract class SqlQueryJoin:
    ISqlQueryPart
{
    protected readonly IList<ISqlQueryPart> SqlParts;
    private readonly JoinType _joinType;
    private readonly Expression _join;
    private readonly Type _type;
    private readonly ISqlQuerySelect? _select;
    public SqlPartType PartType => SqlPartType.Join;

    protected SqlQueryJoin(
        IList<ISqlQueryPart> sqlParts,
        JoinType joinType,
        Expression join,
        Type type,
        ISqlQuerySelect? select = null)
    {
        SqlParts = sqlParts;
        _joinType = joinType;
        _join = join;
        _type = type;
        _select = select;
        
        SqlParts.Add(this);
    }
    
    public string Compute(ISqlQueryTranslator translator)
    {
        var joinBuilder = new StringBuilder();
        switch (_joinType)
        {
            case JoinType.Inner:
                joinBuilder.Append("INNER JOIN");
                break;
            case JoinType.Left:
                joinBuilder.Append("LEFT OUTER JOIN");
                break;
            case JoinType.Right:
                joinBuilder.Append("RIGHT OUTER JOIN");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        joinBuilder.Append(' ');
        if (_select != null)
        {
            joinBuilder.Append(Environment.NewLine);
            joinBuilder.Append('(');
            joinBuilder.Append(Environment.NewLine);
            joinBuilder.Append(string.Join($"{Environment.NewLine} ", _select.Select(s => s.Compute(translator))));
            joinBuilder.Append(Environment.NewLine);
            joinBuilder.Append(')');
            joinBuilder.Append(" AS ");
            joinBuilder.Append(translator.GetTableAlias(_type));
        }
        else
        {
            joinBuilder.Append(translator.Compute(Expression.Constant(_type)));
        }
        
        joinBuilder.Append(" ON ");
        joinBuilder.Append(translator.Compute(_join));

        return joinBuilder.ToString();
    }
}

public class SqlQueryJoin<T, TJ1> : 
    SqlQueryJoin,
    ISqlQueryJoin<T, TJ1>
    where T : ISqlEntity
    where TJ1 : ISqlEntity
{
    internal SqlQueryJoin(
        IList<ISqlQueryPart> sqlParts,
        JoinType joinType,
        Expression join,
        ISqlQuerySelect? select = null)
        : base(sqlParts, joinType, join, typeof(TJ1), select)
    {
    }

    public ISqlQueryJoin<T, TJ1> InnerJoin<TJ2>(Expression<Func<T, TJ1, TJ2, bool>> join) where TJ2 : ISqlEntity
    {
        return new SqlQueryJoin<T, TJ1>(SqlParts, JoinType.Inner, join);
    }

    public ISqlQueryJoin<T, TJ1, TJ2> InnerJoin<TJ2>(ISqlQuerySelect<TJ2> select, Expression<Func<T, TJ1, TJ2, bool>> join) where TJ2 : ISqlEntity
    {
        return new SqlQueryJoin<T, TJ1, TJ2>(SqlParts, JoinType.Inner, join, select);
    }

    public ISqlQueryJoin<T, TJ1> LeftJoin<TJ2>(Expression<Func<T, TJ1, TJ2, bool>> join) where TJ2 : ISqlEntity
    {
        return new SqlQueryJoin<T, TJ1>(SqlParts, JoinType.Left, join);
    }
    
    public ISqlQueryJoin<T, TJ1, TJ2> LeftJoin<TJ2>(ISqlQuerySelect<TJ2> select, Expression<Func<T, TJ1, TJ2, bool>> join) where TJ2 : ISqlEntity
    {
        return new SqlQueryJoin<T, TJ1, TJ2>(SqlParts, JoinType.Left, join, select);
    }

    public ISqlQueryJoin<T, TJ1> RightJoin<TJ2>(Expression<Func<T, TJ1, TJ2, bool>> join) where TJ2 : ISqlEntity
    {
        return new SqlQueryJoin<T, TJ1>(SqlParts, JoinType.Right, join);
    }

    public ISqlQueryJoin<T, TJ1, TJ2> RightJoin<TJ2>(ISqlQuerySelect<TJ2> select, Expression<Func<T, TJ1, TJ2, bool>> join) where TJ2 : ISqlEntity
    {
        return new SqlQueryJoin<T, TJ1, TJ2>(SqlParts, JoinType.Right, join, select);
    }

    public ISqlQueryOrder<T, TJ1> OrderBy(Expression<Func<T, TJ1, object>> orderBy, params Expression<Func<T, TJ1, object>>[] otherOrderBy)
    {
        return new SqlQueryOrder<T, TJ1>(SqlParts, orderBy, otherOrderBy);
    }

    public ISqlQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TS>> select) where TS : ISqlEntity
    {
        return new SqlQuerySelect<TS>(SqlParts, select.Merge());
    }

    public ISqlQuerySelect<T> Select(Expression<Func<T, object>> select, params Expression<Func<T, object>>[] otherSelect)
    {
        return new SqlQuerySelect<T>(SqlParts, select.Merge(otherSelect));
    }

    public ISqlQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TS, object>> select, params Expression<Func<T, TJ1, TS, object>>[] otherSelect) where TS: ISqlEntity
    {
        return new SqlQuerySelect<TS>(SqlParts, select.Merge(otherSelect));
    }
}

public class SqlQueryJoin<T, TJ1, TJ2> : SqlQueryJoin,
        ISqlQueryJoin<T, TJ1, TJ2>
        where T : ISqlEntity
        where TJ1 : ISqlEntity
        where TJ2 : ISqlEntity
    {
        public SqlQueryJoin(
            IList<ISqlQueryPart> sqlParts,
            JoinType joinType,
            Expression join,
            ISqlQuerySelect? select = null)
            : base(sqlParts, joinType, join, typeof(TJ2), select)
        {
        }

        public ISqlQueryJoin<T, TJ1, TJ2> InnerJoin<TJ3>(Expression<Func<T, TJ1, TJ2, TJ3, bool>> join) where TJ3 : ISqlEntity
        {
            return new SqlQueryJoin<T, TJ1, TJ2>(SqlParts, JoinType.Inner, join);
        }

        public ISqlQueryJoin<T, TJ1, TJ2> InnerJoin<TJ3>(ISqlQuerySelect<TJ2> select, Expression<Func<T, TJ1, TJ2, TJ3, bool>> join) where TJ3 : ISqlEntity
        {
            return new SqlQueryJoin<T, TJ1, TJ2>(SqlParts, JoinType.Inner, join, select);
        }

        public ISqlQueryJoin<T, TJ1, TJ2> LeftJoin<TJ3>(Expression<Func<T, TJ1, TJ2, TJ3, bool>> join) where TJ3 : ISqlEntity
        {
            return new SqlQueryJoin<T, TJ1, TJ2>(SqlParts, JoinType.Left, join);
        }

        public ISqlQueryJoin<T, TJ1, TJ2> LeftJoin<TJ3>(ISqlQuerySelect<TJ2> select, Expression<Func<T, TJ1, TJ2, TJ3, bool>> join) where TJ3 : ISqlEntity
        {
            return new SqlQueryJoin<T, TJ1, TJ2>(SqlParts, JoinType.Left, join, select);
        }

        public ISqlQueryJoin<T, TJ1, TJ2> RightJoin<TJ3>(Expression<Func<T, TJ1, TJ2, TJ3, bool>> join) where TJ3 : ISqlEntity
        {
            return new SqlQueryJoin<T, TJ1, TJ2>(SqlParts, JoinType.Right, join);
        }

        public ISqlQueryJoin<T, TJ1, TJ2> RightJoin<TJ3>(ISqlQuerySelect<TJ2> select, Expression<Func<T, TJ1, TJ2, TJ3, bool>> join) where TJ3 : ISqlEntity
        {
            return new SqlQueryJoin<T, TJ1, TJ2>(SqlParts, JoinType.Right, join, select);
        }

        public ISqlQueryOrder<T, TJ1, TJ2> OrderBy(Expression<Func<T, TJ1, TJ2, object>> orderBy, params Expression<Func<T, TJ1, TJ2, object>>[] otherOrderBy)
        {
            return new SqlQueryOrder<T, TJ1, TJ2>(SqlParts, orderBy, otherOrderBy);
        }
    }