namespace SqlQueryBuilder.Services;

public interface ITableAliasPrefix
{
    string Prefix { get; }
}

internal class DefaultTableAliasPrefix : ITableAliasPrefix
{
    public string Prefix => "_t";
}