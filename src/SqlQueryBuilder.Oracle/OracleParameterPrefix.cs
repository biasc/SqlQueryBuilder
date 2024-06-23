using SqlQueryBuilder.Services;

namespace SqlQueryBuilder.Oracle;

public class OracleParameterPrefix : IParameterPrefix
{
    public string ApplyPrefix(string parameterName) => $":{parameterName}";
}