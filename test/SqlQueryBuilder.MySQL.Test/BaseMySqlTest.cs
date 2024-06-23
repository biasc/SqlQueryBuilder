namespace SqlQueryBuilder.MySQL.Test;

public abstract class BaseMySqlTest
{
    protected BaseMySqlTest()
    {
        MySqlOptions.Initialize();
    }
}