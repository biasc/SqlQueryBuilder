using System.Collections;
using SqlQueryBuilder.Abstractions;
using SqlQueryBuilder.Abstractions.Parts;

namespace SqlQueryBuilder.Parts;

public class SqlQueryDelete<T> : ISqlQueryDelete<T>
    where T : ISqlEntity
{
    private readonly IQueryBuilderOptions _queryBuilderOptions;
    private readonly IList<ISqlQueryPart> _sqlParts;

    public SqlQueryDelete(IQueryBuilderOptions queryBuilderOptions, IList<ISqlQueryPart>? sqlParts = null)
    {
        ArgumentNullException.ThrowIfNull(queryBuilderOptions);
        
        _queryBuilderOptions = queryBuilderOptions;
        _sqlParts = sqlParts ?? new List<ISqlQueryPart>();
        
        _sqlParts.Add(this);
    }

    public SqlPartType PartType => SqlPartType.Delete;
    
    public string Compute(ISqlQueryTranslator translator)
    {
        return "DELETE";
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    public IEnumerator<ISqlQueryPart> GetEnumerator()
    {
        foreach (var fragment in _sqlParts.OrderBy(f => f, new SqlQueryPartComparer(PartType)))
        {
            yield return fragment;
        }
    }
}