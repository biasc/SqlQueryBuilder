using SqlQueryBuilder.Abstractions;

namespace SqlQueryBuilder.Test.Common.Models.Northwind;

public record CategoryProductProjection : ISqlEntity
{
    public int ProductID { get; set; }
    public string ProductName { get; set; }
    public int CategoryID { get; set; }
    public string CategoryName { get; set; }
}