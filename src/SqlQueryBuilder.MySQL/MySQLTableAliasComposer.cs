using SqlQueryBuilder.Services;

namespace SqlQueryBuilder.MySQL;

public sealed class MySQLTableAliasComposer: ITableAliasComposer
{
    public string ApplyTableAlias(string tableName, string tableAlias)
    {
        return $"{tableName} AS {tableAlias}";
    }
}