using SqlQueryBuilder.Services;

namespace SqlQueryBuilder.Oracle;

public sealed class OracleTableAliasPrefix : ITableAliasPrefix
{
    public string Prefix => "tAlias";
}