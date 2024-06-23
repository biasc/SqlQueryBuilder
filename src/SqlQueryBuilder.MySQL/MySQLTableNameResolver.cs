using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using SqlQueryBuilder.Services;
using SqlQueryBuilder.MySQL.Attributes;

namespace SqlQueryBuilder.MySQL;

public sealed class MySQLTableNameResolver : ITableNameResolver
{
    public string GetTableName(Type type)
    {
        var customTableAttribute = type.GetCustomAttribute<MySQLTableAttribute>() ??
                                   type.GetCustomAttribute<TableAttribute>();

        return customTableAttribute?.Name?? type.Name;
    }
}