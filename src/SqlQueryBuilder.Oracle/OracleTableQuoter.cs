using SqlQueryBuilder.Services;

namespace SqlQueryBuilder.Oracle;

public sealed class OracleTableQuoter: ITableQuoter
{
    public string Quote(string tableName) => $"\"{tableName}\"";
}