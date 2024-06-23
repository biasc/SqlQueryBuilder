using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using SqlQueryBuilder.Services;
using SqlQueryBuilder.MySQL.Attributes;

namespace SqlQueryBuilder.MySQL;

public sealed class MySQLColumnNameResolver : IColumnNameResolver
{
    public string GetColumnName(MemberInfo member)
    {
        var columnAttribute = member.GetCustomAttribute<MySQLColumnAttribute>() ?? 
                              member.GetCustomAttribute<ColumnAttribute>();
        
        return columnAttribute?.Name ?? member.Name;
    }
}