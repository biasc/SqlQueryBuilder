using System.ComponentModel.DataAnnotations.Schema;
using SqlQueryBuilder.Abstractions;

namespace SqlQueryBuilder.Test.Common.Models.Northwind;

[Table("Order Details")]
public record Order_Details : ISqlEntity
{
	[Column("OrderID")]
	public int OrderID { get; set; }

	[Column("ProductID")]
	public int ProductID { get; set; }

	[Column("UnitPrice")]
	public decimal UnitPrice { get; set; }

	[Column("Quantity")]
	public short Quantity { get; set; }

	[Column("Discount")]
	public float Discount { get; set; }
}