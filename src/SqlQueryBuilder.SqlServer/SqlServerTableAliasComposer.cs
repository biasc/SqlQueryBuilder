using SqlQueryBuilder.Services;

namespace SqlQueryBuilder.SqlServer;

public sealed class SqlServerTableAliasComposer: ITableAliasComposer
{
    public string ApplyTableAlias(string tableName, string tableAlias)
    {
        return $"{tableName} AS {tableAlias}";
    }
}