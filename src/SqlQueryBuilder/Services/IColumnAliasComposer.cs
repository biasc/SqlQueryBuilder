namespace SqlQueryBuilder.Services;

public interface IColumnAliasComposer
{
    string SetAlias(string columnName, string columnAlias);
}

internal class DefaultColumnAliasComposer : IColumnAliasComposer
{
    public string SetAlias(string columnName, string columnAlias)
    {
        return $"{columnName} {columnAlias}";
    }
}