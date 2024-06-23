using SqlQueryBuilder.Services;

namespace SqlQueryBuilder.MySQL;

public sealed class MySQLTableQuoter: ITableQuoter
{
    public string Quote(string tableName) => $"`{tableName}`";
}