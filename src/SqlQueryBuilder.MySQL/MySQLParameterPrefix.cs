using SqlQueryBuilder.Services;

namespace SqlQueryBuilder.MySQL;

public class MySQLParameterPrefix : IParameterPrefix
{
    public string ApplyPrefix(string parameterName) => $"?{parameterName}";
}