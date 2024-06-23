using SqlQueryBuilder.Services;

namespace SqlQueryBuilder.Oracle;

public sealed class OracleTableAliasComposer: ITableAliasComposer
{
    public string ApplyTableAlias(string tableName, string tableAlias)
    {
        return $"{tableName} {tableAlias}";
    }
}