using SqlQueryBuilder.Services;

namespace SqlQueryBuilder.MySQL;

public sealed class MySQLColumnQuoter: IColumnQuoter
{
    public string Quote(string columnName) => $"`{columnName}`";
}