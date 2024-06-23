using SqlQueryBuilder.Services;

namespace SqlQueryBuilder;

public sealed class DefaultQueryBuilderOptions : IQueryBuilderOptions
{
    public ITableAliasComposer TableAliasComposer { get; } = new DefaultTableAliasComposer();
    public ITableAliasPrefix TableAliasPrefix { get; } = new DefaultTableAliasPrefix();
    public ITableQuoter TableQuoter { get; } = new DefaultTableQuoter();
    public IColumnAliasComposer ColumnAliasComposer { get; } = new DefaultColumnAliasComposer();
    public IColumnQuoter ColumnQuoter { get; } = new DefaultColumnQuoter();
    public IParameterPrefix ParameterPrefix { get; } = new DefaultParameterPrefix();
    public ISqlEntityParser SqlEntityParser { get; } = new DefaultSqlEntityParser();
}

public interface IQueryBuilderOptions
{
    // Tables
    ITableAliasComposer TableAliasComposer { get; }
    ITableAliasPrefix TableAliasPrefix { get; }
    ITableQuoter TableQuoter { get; }
    
    // Columns
    IColumnAliasComposer ColumnAliasComposer { get; }
    IColumnQuoter ColumnQuoter { get; }
    
    // Parameters
    IParameterPrefix ParameterPrefix { get; }
    
    // Parsers
    ISqlEntityParser SqlEntityParser { get; }
}
