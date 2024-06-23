namespace SqlQueryBuilder.Services;

public interface IParameterPrefix
{
    string ApplyPrefix(string parameterName);
}

internal class DefaultParameterPrefix : IParameterPrefix
{
    public string ApplyPrefix(string parameterName)
    {
        return parameterName;
    }
}