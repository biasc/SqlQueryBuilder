using System.Collections;
using System.Linq.Expressions;
using SqlQueryBuilder.Abstractions;
using SqlQueryBuilder.Abstractions.Extensions;
using SqlQueryBuilder.Abstractions.Parts;

namespace SqlQueryBuilder.Parts;

public class SqlQueryOrder<T> : ISqlQueryOrder<T>
    where T : ISqlEntity
{
    private readonly IList<ISqlQueryPart> _sqlParts;
    private readonly Expression[] _orderBy;
    
    internal SqlQueryOrder(IList<ISqlQueryPart>? sqlParts, Expression orderBy, Expression[] otherOrderBy)
    {
        _sqlParts = sqlParts ?? new List<ISqlQueryPart>();
        _orderBy = orderBy.Merge(otherOrderBy);

        _sqlParts.Add(this);
    }


    public SqlPartType PartType => SqlPartType.Order;
    public string Compute(ISqlQueryTranslator translator)
    {
        return $"ORDER BY {string.Join(", ", _orderBy.Select(o => translator.Compute(o)))}";
    }

    public IEnumerator<ISqlQueryPart> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
    
    public ISqlQuerySelect<T> Select<TS>(Expression<Func<T, TS>> select)
    {
        return new SqlQuerySelect<T>(_sqlParts, [select]);
    }

    public ISqlQuerySelect<T> Select(Expression<Func<T, object?>> select, params Expression<Func<T, object?>>[] otherSelect)
    {
        if (select == null)
            throw new ArgumentNullException(nameof(select));
        
        return new SqlQuerySelect<T>(_sqlParts, select.Merge(otherSelect));
    }
}