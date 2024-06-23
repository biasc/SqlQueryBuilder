namespace SqlQueryBuilder.MySQL;

public static class MySqlOptions
{
    public static void Initialize()
    {
        QueryOptions.TableNameResolver = new MySQLTableNameResolver();
        QueryOptions.ColumnNameResolver = new MySQLColumnNameResolver();
        QueryOptions.ParameterPrefix = new MySQLParameterPrefix();
        QueryOptions.ColumnQuoter = new MySQLColumnQuoter();
        QueryOptions.TableQuoter = new MySQLTableQuoter();
        QueryOptions.TableAliasComposer = new MySQLTableAliasComposer();
        QueryOptions.ColumnAliasComposer = new MySQLColumnAliasComposer();
    }
}