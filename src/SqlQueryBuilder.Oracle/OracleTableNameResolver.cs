using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using SqlQueryBuilder.Services;
using SqlQueryBuilder.Oracle.Attributes;

namespace SqlQueryBuilder.Oracle;

public sealed class OracleTableNameResolver : ITableNameResolver
{
    public string GetTableName(Type type)
    {
        var customTableAttribute = type.GetCustomAttribute<OracleTableAttribute>() ??
                                   type.GetCustomAttribute<TableAttribute>();

        return customTableAttribute?.Name?? type.Name;
    }
}