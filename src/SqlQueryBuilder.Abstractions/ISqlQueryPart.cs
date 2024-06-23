namespace SqlQueryBuilder.Abstractions;

public interface ISqlQueryPart
{
    SqlPartType PartType { get; }

    string Compute(ISqlQueryTranslator translator);
}

public enum SqlPartType
{
    Insert,
    InsertValues,
    Delete,
    Update,
    Truncate,
    Drop,
    Select,
    Into,
    From,
    FromBySelect,
    Join,
    JoinBySelect,
    Where,
    Group,
    GroupHaving,
    Union,
    Page, 
    Order,
    Batch
}