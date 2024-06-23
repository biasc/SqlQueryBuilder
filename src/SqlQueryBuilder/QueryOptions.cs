using SqlQueryBuilder.Services;

namespace SqlQueryBuilder;

public static class QueryOptions
{
    static QueryOptions()
    {
        ParameterPrefix = new DefaultParameterPrefix();
        TableQuoter = new DefaultTableQuoter();
        ColumnQuoter = new DefaultColumnQuoter();
        TableAliasComposer = new DefaultTableAliasComposer();
        ColumnAliasComposer = new DefaultColumnAliasComposer();
        TableAliasPrefix = new DefaultTableAliasPrefix();
        ColumnNameResolver = new DefaultColumnNameResolver();
        TableNameResolver = new DefaultTableNameResolver();
    }
    
    public static ITableAliasPrefix TableAliasPrefix { get; set; }
    public static IParameterPrefix ParameterPrefix { get; set; }
    public static ITableQuoter TableQuoter { get; set; }
    public static IColumnQuoter ColumnQuoter { get; set; }
    public static IColumnNameResolver ColumnNameResolver { get; set; }
    public static ITableNameResolver TableNameResolver { get; set; }
    public static ITableAliasComposer TableAliasComposer { get; set; }
    public static IColumnAliasComposer ColumnAliasComposer { get; set; }
}
