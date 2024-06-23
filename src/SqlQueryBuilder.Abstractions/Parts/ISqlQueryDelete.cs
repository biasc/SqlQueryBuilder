namespace SqlQueryBuilder.Abstractions.Parts;

public interface ISqlQueryDelete<T> :
    ISqlQueryPart, ISqlQuery
    where T : ISqlEntity
{
    
}