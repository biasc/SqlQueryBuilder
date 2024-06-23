namespace SqlQueryBuilder;

public class SqlQueryCompiled
{
    internal SqlQueryCompiled(
        string statement,
        IList<SqlQueryParameter> parameters)
    {
        Statement = statement;
        Parameters = parameters;
    }

    /// <summary>
    /// The TSQL statement.
    /// </summary>
    public string Statement { get; }

    /// <summary>
    /// The list of parameters.
    /// </summary>
    public IList<SqlQueryParameter> Parameters { get; }


}