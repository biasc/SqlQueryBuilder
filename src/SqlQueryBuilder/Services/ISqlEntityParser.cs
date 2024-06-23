using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace SqlQueryBuilder.Services;

public interface ISqlEntityParser
{
    SqlEntityTableInfo GetTableInfo(Type objType);
    SqlEntityColumnInfo GetColumnInfo(Type objType, MemberInfo member);
}

public record SqlEntityTableInfo(string Name, string Alias);
public record SqlEntityColumnInfo(string Name);


internal class DefaultSqlEntityParser: ISqlEntityParser
{
    private static ConcurrentDictionary<Type, SqlEntityTableInfo> _alias { get; } = new ();

    private static ConcurrentDictionary<Type, IDictionary<MemberInfo, SqlEntityColumnInfo>> _memberTypes { get; } = new ();
    
    public SqlEntityTableInfo GetTableInfo(Type objType)
    {
        if (_alias.TryGetValue(objType, out var tableInfo))
            return tableInfo;
            
        var tableAttribute = objType.GetCustomAttribute<TableAttribute>();
        var tableName = tableAttribute?.Name?? objType.Name;
            
        tableInfo = new SqlEntityTableInfo(QueryOptions.TableQuoter.Quote(tableName), $"{QueryOptions.TableAliasPrefix.Prefix}{_alias.Count}" );

        _alias.TryAdd(objType, tableInfo);
        return tableInfo;
    }

    public SqlEntityColumnInfo GetColumnInfo(Type objType, MemberInfo member)
    {
        ArgumentNullException.ThrowIfNull(member?.DeclaringType);

        if (!_memberTypes.TryGetValue(member.DeclaringType, out var members))
        {
            members = new ConcurrentDictionary<MemberInfo, SqlEntityColumnInfo>();
            _memberTypes.TryAdd(member.DeclaringType, members);
        }

        if (members.TryGetValue(member, out var columnInfo)) 
            return columnInfo;

        var columnName = GetMemberName(member);
        columnName = QueryOptions.ColumnQuoter.Quote(columnName);

        columnInfo = new SqlEntityColumnInfo(columnName);
        
            
        members.Add(member, columnInfo);

        return columnInfo;
    }
    
    private string GetMemberName(MemberInfo member)
    {
        var columnAttribute = member.GetCustomAttribute<ColumnAttribute>();
        return columnAttribute?.Name ?? member.Name;
    }
}