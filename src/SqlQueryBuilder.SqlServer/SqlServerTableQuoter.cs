using SqlQueryBuilder.Services;

namespace SqlQueryBuilder.SqlServer;

public sealed class SqlServerTableQuoter: ITableQuoter
{
    public string Quote(string tableName) => $"[{tableName}]";
}