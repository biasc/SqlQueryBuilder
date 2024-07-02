using System.Collections;
using System.Linq.Expressions;
using System.Text;
using SqlQueryBuilder.Abstractions;
using SqlQueryBuilder.Abstractions.Parts;

namespace SqlQueryBuilder.Parts;

public class SqlQuerySelect<T> : 
    ISqlQuerySelect<T>
    where T : ISqlEntity
{
    private readonly Expression[]? _select;
    private readonly IList<ISqlQueryPart> _sqlParts;
    private bool _count;
    private bool _distinct;
    
    public SqlPartType PartType => SqlPartType.Select;

    public SqlQuerySelect(IList<ISqlQueryPart>? sqlParts = null, Expression[]? select = null)
    {
        _select = select;
        _sqlParts = sqlParts ?? new List<ISqlQueryPart>();
        
        _sqlParts.Add(this);
    }
    
    public string Compute(ISqlQueryTranslator translator)
    {
        var selectBase = new StringBuilder();

        selectBase.Append("SELECT ");

        if (_count)
        {
            if (_distinct)
            {
                if (_select != null && _select.Length > 0)
                {
                    selectBase.Append($"{Environment.NewLine}COUNT(DISTINCT {string.Join($", ", _select.Select(s => translator.Compute(s)))})");
                }
                else
                {
                    selectBase.Append("COUNT(DISTINCT *)");
                }
            }
            else
            {
                if (_select != null && _select.Length > 0)
                {
                    selectBase.Append($"{Environment.NewLine}COUNT({string.Join($", ", _select.Select(s => translator.Compute(s)))})");
                }
                else
                {
                    selectBase.Append("COUNT(*)");
                }

            }
        }
        else if (_distinct)
        {
            selectBase.Append("DISTINCT ");


            if (_select != null && _select.Length > 0)
            {
                selectBase.Append($"{Environment.NewLine}{string.Join($", {Environment.NewLine}", _select.Select(s => translator.Compute(s)))}");
            }
            else
            {
                selectBase.Append("*");
            }
        }
        else
        {
            if (_select != null && _select.Length > 0)
            {
                selectBase.Append($"{Environment.NewLine}{string.Join($", {Environment.NewLine}", _select.Select(s => translator.Compute(s)))}");
            }
            else
            {
                selectBase.Append("*");
            }
        }

        return selectBase.ToString();
    }

    public IEnumerator<ISqlQueryPart> GetEnumerator()
    {
        foreach (var fragment in _sqlParts.OrderBy(f => f, new SqlQueryPartComparer(PartType)))
        {
            yield return fragment;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public ISqlQuerySelect Count()
    {
        _count = true;
        return this;
    }

    public ISqlQuerySelect Distinct()
    {
        _distinct = true;
        return this;
    }
}