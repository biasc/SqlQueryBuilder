namespace SqlQueryBuilder.Oracle;

public static class OracleOptions
{
    public static void Initialize()
    {
        QueryOptions.TableNameResolver = new OracleTableNameResolver();
        QueryOptions.ColumnNameResolver = new OracleColumnNameResolver();
        QueryOptions.ParameterPrefix = new OracleParameterPrefix();
        QueryOptions.ColumnQuoter = new OracleColumnQuoter();
        QueryOptions.TableQuoter = new OracleTableQuoter();
        QueryOptions.TableAliasComposer = new OracleTableAliasComposer();
        QueryOptions.ColumnAliasComposer = new OracleColumnAliasComposer();
        QueryOptions.TableAliasPrefix = new OracleTableAliasPrefix();
    }
}