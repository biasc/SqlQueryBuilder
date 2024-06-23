using System.Collections;
using System.Linq.Expressions;
using System.Text;
using SqlQueryBuilder.Abstractions;
using SqlQueryBuilder.Abstractions.Extensions;
using SqlQueryBuilder.Abstractions.Parts;

namespace SqlQueryBuilder.Parts;

public class SqlQueryFrom<T> : 
    ISqlQueryPart,
    ISqlQueryWithSelect<T>,
    ISqlQueryFrom<T>
    where T : ISqlEntity
{
    private readonly IQueryBuilderOptions _queryBuilderOptions;
    private readonly ISqlQuerySelect<T>? _select;
    private readonly IList<ISqlQueryPart> _sqlParts;

    internal SqlQueryFrom(IQueryBuilderOptions queryBuilderOptions, IList<ISqlQueryPart>? parts = null, ISqlQuerySelect<T>? select = null)
    {
        ArgumentNullException.ThrowIfNull(queryBuilderOptions);
        
        _sqlParts = parts ?? new List<ISqlQueryPart>();
        _queryBuilderOptions = queryBuilderOptions;
        _select = select;
        _sqlParts.Add(this);
    }

    public SqlPartType PartType => _select != null 
        ? SqlPartType.FromBySelect 
        : SqlPartType.From;
    
    public ISqlQuerySelect<T> SelectAll()
    {
        throw new NotImplementedException();
    }

    public string Compute(ISqlQueryTranslator translator)
    {
        var fromBuilder = new StringBuilder();

        if (_select != null)
        {
            if (_sqlParts.All(f => f.PartType != SqlPartType.Insert))
            {
                fromBuilder.Append($"FROM {Environment.NewLine}({Environment.NewLine}{string.Join($"{Environment.NewLine} ", _select.Select(s => s.Compute(translator)))}{Environment.NewLine}) AS {translator.GetTableAlias(typeof(T))}");
            }
        }
        else if (_sqlParts.All(f => f.PartType != SqlPartType.Insert && f.PartType != SqlPartType.Update))
        {
            if (_sqlParts.Any(f => f.PartType == SqlPartType.Select))
            {
                fromBuilder.Append($"FROM {translator.Compute(Expression.Constant(typeof(T)))}");
            }
            else
            {
                fromBuilder.Append($"FROM {translator.GetTableName(typeof(T))}");
            }
        }

        return fromBuilder.ToString();
    }
    //
    // public IEnumerator<ISqlQueryPart> GetEnumerator()
    // {
    //     throw new NotImplementedException();
    // }
    //
    // IEnumerator IEnumerable.GetEnumerator()
    // {
    //     return GetEnumerator();
    // }
    
    public ISqlQuerySelect<T> Select<TS>(Expression<Func<T, TS>> select)
    {
        return new SqlQuerySelect<T>(_sqlParts, [select]);
    }

    public ISqlQuerySelect<T> Select(Expression<Func<T, object?>> select, params Expression<Func<T, object?>>[] otherSelect)
    {
        var merged = select.Merge(otherSelect);
        return new SqlQuerySelect<T>(_sqlParts, merged);
    }

    public ISqlQuerySelect<TS> Select<TS>(Expression<Func<T, TS, object>> select, params Expression<Func<T, TS, object>>[] otherSelect)
    {
        throw new NotImplementedException();
    }

    public ISqlQueryWhere<T> Where(Expression<Func<T, bool>> where)
    {
        return new SqlQueryWhere<T>(_queryBuilderOptions,_sqlParts, where);
    }

    public ISqlQueryDelete<T> Delete()
    {
        return new SqlQueryDelete<T>(_queryBuilderOptions,_sqlParts);
    }

    public ISqlQueryOrder<T> OrderBy(
        Expression<Func<T, object>> orderBy,
        params Expression<Func<T, object>>[] otherOrderBy)
    {
         return new SqlQueryOrder<T>(_sqlParts, orderBy, otherOrderBy);
    }
}