namespace SqlQueryBuilder.Services;

public interface IColumnQuoter
{
    string Quote(string columnName);
}

internal class DefaultColumnQuoter : IColumnQuoter
{
    public string Quote(string columnName) => columnName;
}