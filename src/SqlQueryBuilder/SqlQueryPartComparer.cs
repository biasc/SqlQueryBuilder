using System.Collections.Immutable;
using SqlQueryBuilder.Abstractions;

namespace SqlQueryBuilder;

internal class SqlQueryPartComparer : IComparer<ISqlQueryPart>
{
    private static readonly ImmutableDictionary<SqlPartType, IDictionary<SqlPartType, int>> Order;
        
    static SqlQueryPartComparer()
    {
        var order = new Dictionary<SqlPartType, IDictionary<SqlPartType, int>>
        {
            { SqlPartType.Insert, GetInsertOrder() },
            { SqlPartType.Delete, GetDeleteOrder() },
            { SqlPartType.Update, GetUpdateOrder() },
            { SqlPartType.Truncate, GetTruncateOrder() },
            { SqlPartType.Drop, GetDropOrder() },
            { SqlPartType.Union, GetUnionOrder() },
            { SqlPartType.Select, GetDefaultOrder() },
            { SqlPartType.Into, GetDefaultOrder() },
            { SqlPartType.Batch, GetBatchOrder() }
        };

        Order = order.ToImmutableDictionary();
    }

    private readonly IDictionary<SqlPartType, int>? _sqlParts;

    public SqlQueryPartComparer(SqlPartType callerPart)
    {
        if (!Order.TryGetValue(callerPart, out _sqlParts))
        {
            throw new InvalidOperationException($"SqlPart order for {callerPart} is not supported");
        }
    }

    public int Compare(ISqlQueryPart? x, ISqlQueryPart? y)
    {
        if (x == null && y == null)
            return 0;
        
        if (y != null && x == null)
            return 1;
        
        if (x != null && y == null)
            return -1;
        
        if (_sqlParts == null)
            return 0;
        
        if (!_sqlParts.TryGetValue(x!.PartType, out int xOrder) ||
            !_sqlParts.TryGetValue(y!.PartType, out int yOrder))
        {
            throw new InvalidOperationException($"SqlPart order between {x!.PartType} and {y!.PartType} is not supported");
        }

        return xOrder.CompareTo(yOrder);
    }

    static IDictionary<SqlPartType, int> GetInsertOrder()
    {
        var partsOrder = new Dictionary<SqlPartType, int>
        {
            { SqlPartType.Insert, 0 },
            { SqlPartType.InsertValues, 3 },
            { SqlPartType.Select, 3 },
            { SqlPartType.From, 4 },
            { SqlPartType.FromBySelect, 4 }
        };

        return partsOrder.ToImmutableDictionary();
    }

    static IDictionary<SqlPartType, int> GetDeleteOrder()
    {
        var partsOrder = new Dictionary<SqlPartType, int>
        {
            { SqlPartType.Delete, 0 },
            { SqlPartType.From, 1 },
            { SqlPartType.Select, 3 },
            { SqlPartType.FromBySelect, 4 },
            { SqlPartType.Where, 5 }
        };

        return partsOrder.ToImmutableDictionary();
    }

    static IDictionary<SqlPartType, int> GetUpdateOrder()
    {
        var partsOrder = new Dictionary<SqlPartType, int>
        {
            { SqlPartType.Update, 0 },
            { SqlPartType.Select, 2 },
            { SqlPartType.From, 3 },
            { SqlPartType.FromBySelect, 3 },
            { SqlPartType.Join, 4 },
            { SqlPartType.JoinBySelect, 4 },
            { SqlPartType.Where, 5 },
            { SqlPartType.Group, 6 },
            { SqlPartType.GroupHaving, 7 }
        };

        return partsOrder.ToImmutableDictionary();
    }

    static IDictionary<SqlPartType, int> GetTruncateOrder()
    {
        var partsOrder = new Dictionary<SqlPartType, int>
        {
            { SqlPartType.Truncate, 0 }
        };

        return partsOrder.ToImmutableDictionary();
    }

    static IDictionary<SqlPartType, int> GetDropOrder()
    {
        var partsOrder = new Dictionary<SqlPartType, int>
        {
            { SqlPartType.Drop, 0 }
        };

        return partsOrder.ToImmutableDictionary();
    }

    static IDictionary<SqlPartType, int> GetUnionOrder()
    {
        var partsOrder = new Dictionary<SqlPartType, int>
        {
            { SqlPartType.Union, 0 }
        };

        return partsOrder.ToImmutableDictionary();
    }

    static IDictionary<SqlPartType, int> GetBatchOrder()
    {
        var partsOrder = new Dictionary<SqlPartType, int>
        {
            { SqlPartType.Batch, 0 }
        };

        return partsOrder.ToImmutableDictionary();
    }

    static IDictionary<SqlPartType, int> GetDefaultOrder()
    {            
        var partsOrder = new Dictionary<SqlPartType, int>
        {
            { SqlPartType.Insert, 0 },
            { SqlPartType.InsertValues, 1 },
            { SqlPartType.Delete, 0 },
            { SqlPartType.Update, 0 },
            { SqlPartType.Truncate, 0 },
            { SqlPartType.Drop, 0 },
            { SqlPartType.Select, 2 },
            { SqlPartType.Into, 3 },
            { SqlPartType.From, 4 },
            { SqlPartType.FromBySelect, 4 },
            { SqlPartType.Join, 5 },
            { SqlPartType.JoinBySelect, 5 },
            { SqlPartType.Where, 6 },
            { SqlPartType.Group, 7 },
            { SqlPartType.GroupHaving, 8 },
            { SqlPartType.Union, 9 },
            { SqlPartType.Order, 10 },
            { SqlPartType.Page, 11 },
        };

        return partsOrder.ToImmutableDictionary();
    }
}