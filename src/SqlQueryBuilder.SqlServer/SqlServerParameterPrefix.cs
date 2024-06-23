using SqlQueryBuilder.Services;

namespace SqlQueryBuilder.SqlServer;

public class SqlServerParameterPrefix : IParameterPrefix
{
    public string ApplyPrefix(string parameterName) => $"@{parameterName}";
}