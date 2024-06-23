namespace SqlQueryBuilder.SqlServer;

public static class SqlServerOptions
{
    public static void Initialize()
    {
        QueryOptions.ColumnNameResolver = new SqlServerColumnNameResolver();
        QueryOptions.TableNameResolver = new SqlServerTableNameResolver();
        QueryOptions.ParameterPrefix = new SqlServerParameterPrefix();
        QueryOptions.ColumnQuoter = new SqlServerColumnQuoter();
        QueryOptions.TableQuoter = new SqlServerTableQuoter();
        QueryOptions.TableAliasComposer = new SqlServerTableAliasComposer();
        QueryOptions.ColumnAliasComposer = new SqlServerColumnAliasComposer();
    }
}