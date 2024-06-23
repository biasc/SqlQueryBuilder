namespace SqlQueryBuilder.Services;

public interface ITableAliasComposer
{
    string ApplyTableAlias(string tableName, string tableAlias);
}

internal class DefaultTableAliasComposer : ITableAliasComposer
{
    public string ApplyTableAlias(string tableName, string tableAlias)
    {
        return $"{tableName} {tableAlias}";
    }
}