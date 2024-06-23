namespace SqlQueryBuilder.SqlServer.Test;

public abstract class BaseSqlServerTest
{
    protected BaseSqlServerTest()
    {
        SqlServerOptions.Initialize();
    }
}