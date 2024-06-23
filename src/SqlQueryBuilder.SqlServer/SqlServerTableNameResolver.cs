using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using SqlQueryBuilder.Services;
using SqlQueryBuilder.SqlServer.Attributes;

namespace SqlQueryBuilder.SqlServer;

public sealed class SqlServerTableNameResolver : ITableNameResolver
{
    public string GetTableName(Type type)
    {
        var customTableAttribute = type.GetCustomAttribute<SqlServerTableAttribute>() ??
                                   type.GetCustomAttribute<TableAttribute>();

        return customTableAttribute?.Name?? type.Name;
    }
}