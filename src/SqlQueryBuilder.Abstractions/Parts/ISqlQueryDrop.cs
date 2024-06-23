namespace SqlQueryBuilder.Abstractions.Parts;

public interface ISqlQueryDrop<T> : 
    ISqlQueryPart, ISqlQuery
    where T : ISqlEntity;