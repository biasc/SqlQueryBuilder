using SqlQueryBuilder.Services;

namespace SqlQueryBuilder.Oracle;

public sealed class OracleColumnQuoter: IColumnQuoter
{
    public string Quote(string columnName) => $"\"{columnName}\"";
}