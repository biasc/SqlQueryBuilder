using SqlQueryBuilder.Services;

namespace SqlQueryBuilder.SqlServer;

public sealed class SqlServerColumnAliasComposer : IColumnAliasComposer
{
    public string SetAlias(string columnName, string columnAlias)
    {
        return $"{columnName} AS {columnAlias}";
    }
}