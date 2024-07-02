using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SqlQueryBuilder.Abstractions;

namespace SqlQueryBuilder.Test.Common.Models.Northwind;

[Table("CustomerDemographics")]
public record CustomerDemographics : ISqlEntity
{
	[Required]
	[Column("CustomerTypeID")]
	public string CustomerTypeID { get; set; }

	[Column("CustomerDesc")]
	public string CustomerDesc { get; set; }
}