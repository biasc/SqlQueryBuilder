using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using SqlQueryBuilder.Services;
using SqlQueryBuilder.SqlServer.Attributes;

namespace SqlQueryBuilder.SqlServer;

public sealed class SqlServerColumnNameResolver : IColumnNameResolver
{
    public string GetColumnName(MemberInfo member)
    {
        var columnAttribute = member.GetCustomAttribute<SqlServerColumnAttribute>() ?? 
                              member.GetCustomAttribute<ColumnAttribute>();
        
        return columnAttribute?.Name ?? member.Name;
    }
}