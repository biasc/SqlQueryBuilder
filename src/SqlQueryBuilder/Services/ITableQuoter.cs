namespace SqlQueryBuilder.Services;

public interface ITableQuoter
{
    string Quote(string tableName);
}

internal class DefaultTableQuoter : ITableQuoter
{
    public string Quote(string tableName) => tableName;
}