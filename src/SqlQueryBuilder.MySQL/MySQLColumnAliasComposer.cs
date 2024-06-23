using SqlQueryBuilder.Services;

namespace SqlQueryBuilder.MySQL;

public sealed class MySQLColumnAliasComposer : IColumnAliasComposer
{
    public string SetAlias(string columnName, string columnAlias)
    {
        return $"{columnName} AS {columnAlias}";
    }
}