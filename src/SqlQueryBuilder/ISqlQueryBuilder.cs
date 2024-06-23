using SqlQueryBuilder.Abstractions;
using SqlQueryBuilder.Abstractions.Parts;
using SqlQueryBuilder.Parts;

namespace SqlQueryBuilder;

public interface ISqlQueryBuilder
{
    ISqlQueryFrom<T> From<T>() where T : ISqlEntity;

    //ISqlQueryTruncate<T> Truncate<T>() where T : ISqlEntity;
    ISqlQueryDrop<T> Drop<T>() where T : ISqlEntity;
    ISqlQueryDelete<T> Delete<T>() where T : ISqlEntity;
    ISqlQueryWhere<T> Where<T>() where T : ISqlEntity;
}

public class SqlQueryBuilder : ISqlQueryBuilder
{
    private readonly IQueryBuilderOptions _queryBuilderOptions;

    public SqlQueryBuilder(): this(new DefaultQueryBuilderOptions())
    {
    }

    protected SqlQueryBuilder(IQueryBuilderOptions queryBuilderOptions)
    {
        _queryBuilderOptions = queryBuilderOptions;
        ArgumentNullException.ThrowIfNull(queryBuilderOptions);
    }
    
    public ISqlQueryFrom<T> From<T>() where T : ISqlEntity
    {
        return new SqlQueryFrom<T>(_queryBuilderOptions);
    }
    
    public ISqlQueryDrop<T> Drop<T>() where T : ISqlEntity
    {
        return new SqlQueryDrop<T>(_queryBuilderOptions);
    }
    
    public ISqlQueryDelete<T> Delete<T>() where T : ISqlEntity
    {
        return new SqlQueryDelete<T>(_queryBuilderOptions);
    }
    
    public ISqlQueryWhere<T> Where<T>() where T : ISqlEntity
    {
        return new SqlQueryWhere<T>(_queryBuilderOptions);
    }
}