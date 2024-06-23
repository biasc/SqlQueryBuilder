using SqlQueryBuilder.Services;

namespace SqlQueryBuilder.SqlServer;

public sealed class SqlServerColumnQuoter: IColumnQuoter
{
    public string Quote(string columnName) => $"[{columnName}]";
}