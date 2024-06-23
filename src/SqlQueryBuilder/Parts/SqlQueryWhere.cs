using System.Collections;
using System.Linq.Expressions;
using SqlQueryBuilder.Abstractions;
using SqlQueryBuilder.Abstractions.Extensions;
using SqlQueryBuilder.Abstractions.Parts;

namespace SqlQueryBuilder.Parts;

public class SqlQueryWhere<T> : ISqlQueryWhere<T> 
    where T : ISqlEntity
{
    private readonly IQueryBuilderOptions _queryBuilderOptions;
    private readonly Expression<Func<T, bool>>? _where;
    private readonly IList<ISqlQueryPart> _sqlParts;
    
    public SqlQueryWhere(IQueryBuilderOptions queryBuilderOptions, IList<ISqlQueryPart>? sqlParts = null, Expression<Func<T, bool>>? where = null)
    {
        ArgumentNullException.ThrowIfNull(queryBuilderOptions);
        
        _queryBuilderOptions = queryBuilderOptions;
        _where = where;
        _sqlParts = sqlParts ?? new List<ISqlQueryPart>();
        
        _sqlParts.Add(this);
    }

    public ISqlQueryDelete<T> Delete()
    {
        return new SqlQueryDelete<T>(_queryBuilderOptions, _sqlParts);
    }
    
    public Expression? GetWhere()
    {
        return _where;
    }

    public SqlPartType PartType => SqlPartType.Where;

    public IEnumerator<ISqlQueryPart> GetEnumerator()
    {
        throw new NotImplementedException();
    }
    
    public ISqlQuerySelect<T> Select<TS>(Expression<Func<T, TS>> select)
    {
        return new SqlQuerySelect<T>(_sqlParts, [select]);
    }

    public ISqlQuerySelect<T> Select(Expression<Func<T, object?>> select, params Expression<Func<T, object?>>[] otherSelect)
    {
        var merged = select.Merge(otherSelect);
        return new SqlQuerySelect<T>(_sqlParts, merged);
    }
    

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    public string Compute(ISqlQueryTranslator translator)
    {
        if (_sqlParts.Any(f => 
                f.PartType == SqlPartType.FromBySelect ||
                f.PartType == SqlPartType.Select))
        {
            return $"WHERE {translator.Compute(GetWhere())}";
        }

        return $"WHERE {translator.GetColumnsWithoutTableAlias(translator.Compute(GetWhere()))}";
    }
}