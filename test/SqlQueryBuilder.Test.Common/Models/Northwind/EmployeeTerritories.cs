using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SqlQueryBuilder.Abstractions;

namespace SqlQueryBuilder.Test.Common.Models.Northwind;

[Table("EmployeeTerritories")]
public record EmployeeTerritories : ISqlEntity
{
	[Column("EmployeeID")]
	public int EmployeeID { get; set; }

	[Required]
	[Column("TerritoryID")]
	public string TerritoryID { get; set; }
}