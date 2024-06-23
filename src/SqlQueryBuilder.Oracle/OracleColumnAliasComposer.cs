using SqlQueryBuilder.Services;

namespace SqlQueryBuilder.Oracle;

public sealed class OracleColumnAliasComposer : IColumnAliasComposer
{
    public string SetAlias(string columnName, string columnAlias)
    {
        return $"{columnName} AS {columnAlias}";
    }
}