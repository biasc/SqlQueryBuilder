using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SqlQueryBuilder.Abstractions;

namespace SqlQueryBuilder.Test.Common.Models.Northwind;

[Table("Region")]
public record Region : ISqlEntity
{
	[Column("RegionID")]
	public int RegionID { get; set; }

	[Required]
	[Column("RegionDescription")]
	public string RegionDescription { get; set; }
}