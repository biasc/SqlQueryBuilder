using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace SqlQueryBuilder.Services;

public interface ITableNameResolver
{
    string GetTableName(Type type);
}

internal class DefaultTableNameResolver : ITableNameResolver
{
    public string GetTableName(Type type)
    {
        var tableAttribute = type.GetCustomAttribute<TableAttribute>();
        return tableAttribute?.Name?? type.Name;
    }
}