using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SqlQueryBuilder.Abstractions;

namespace SqlQueryBuilder.Test.Common.Models.Northwind;

[Table("CustomerCustomerDemo")]
public record CustomerCustomerDemo : ISqlEntity
{
	[Required]
	[Column("CustomerID")]
	public string CustomerID { get; set; }

	[Required]
	[Column("CustomerTypeID")]
	public string CustomerTypeID { get; set; }
}