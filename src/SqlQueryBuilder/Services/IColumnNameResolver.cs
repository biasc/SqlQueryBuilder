using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace SqlQueryBuilder.Services;

public interface IColumnNameResolver
{
    string GetColumnName(MemberInfo member);
}

internal class DefaultColumnNameResolver : IColumnNameResolver
{
    public string GetColumnName(MemberInfo member)
    {
        var columnAttribute = member.GetCustomAttribute<ColumnAttribute>();
        return columnAttribute?.Name ?? member.Name;
    }
}