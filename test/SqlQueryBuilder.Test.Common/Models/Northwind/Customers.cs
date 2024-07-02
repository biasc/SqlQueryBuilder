using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SqlQueryBuilder.Abstractions;

namespace SqlQueryBuilder.Test.Common.Models.Northwind;

[Table("Customers")]
public record Customers : ISqlEntity
{
	[Required]
	[Column("CustomerID")]
	public string CustomerID { get; set; }

	[Required]
	[Column("CompanyName")]
	public string CompanyName { get; set; }

	[Column("ContactName")]
	public string ContactName { get; set; }

	[Column("ContactTitle")]
	public string ContactTitle { get; set; }

	[Column("Address")]
	public string Address { get; set; }

	[Column("City")]
	public string City { get; set; }

	[Column("Region")]
	public string Region { get; set; }

	[Column("PostalCode")]
	public string PostalCode { get; set; }

	[Column("Country")]
	public string Country { get; set; }

	[Column("Phone")]
	public string Phone { get; set; }

	[Column("Fax")]
	public string Fax { get; set; }
}