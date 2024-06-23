using SqlQueryBuilder.Abstractions;

namespace SqlQueryBuilder;

public sealed class SqlQueryParameter
{
    internal SqlQueryParameter(
        string name,
        object? value,
        SqlQueryParameterDirection direction = SqlQueryParameterDirection.Input)
    {
        Name = name;
        Value = value;
        Direction = direction;
    }

    public string Name { get; }
    public object? Value { get; }
    public SqlQueryParameterDirection Direction { get; }

    public override string ToString()
    {
        if (Value == null)
            return $"{Name} = NULL";
        
        if (Value is string || Value is DateTime || Value is DateTimeOffset)
        {
            return $"{Name} = '{Value}'";
        }

        return $"{Name} = {Value}";
    }
}