using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SqlQueryBuilder.Abstractions;

namespace SqlQueryBuilder.Test.Common.Models.Northwind;

[Table("Territories")]
public record Territories : ISqlEntity
{
	[Required]
	[Column("TerritoryID")]
	public string TerritoryID { get; set; }

	[Required]
	[Column("TerritoryDescription")]
	public string TerritoryDescription { get; set; }

	[Column("RegionID")]
	public int RegionID { get; set; }
}