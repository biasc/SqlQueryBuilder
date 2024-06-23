using System.Linq.Expressions;

namespace SqlQueryBuilder.Abstractions;

public interface ISqlQueryTranslator
{
    string Compute(Expression? expression);

    string GetParameterName(object value);

    string GetTableAlias(Type tableType);

    string GetTableName(Type tableType);

    string GetColumnsWithoutTableAlias(string columns);
}







