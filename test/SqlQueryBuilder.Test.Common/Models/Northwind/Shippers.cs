using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SqlQueryBuilder.Abstractions;

namespace SqlQueryBuilder.Test.Common.Models.Northwind;

[Table("Shippers")]
public record Shippers : ISqlEntity
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ShipperID")]
	public int ShipperID { get; set; }

	[Required]
	[Column("CompanyName")]
	public string CompanyName { get; set; }

	[Column("Phone")]
	public string Phone { get; set; }
}