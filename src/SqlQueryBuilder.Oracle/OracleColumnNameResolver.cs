using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using SqlQueryBuilder.Services;
using SqlQueryBuilder.Oracle.Attributes;

namespace SqlQueryBuilder.Oracle;

public sealed class OracleColumnNameResolver : IColumnNameResolver
{
    public string GetColumnName(MemberInfo member)
    {
        var columnAttribute = member.GetCustomAttribute<OracleColumnAttribute>() ?? 
                              member.GetCustomAttribute<ColumnAttribute>();
        
        return columnAttribute?.Name ?? member.Name;
    }
}