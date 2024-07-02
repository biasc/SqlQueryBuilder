using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SqlQueryBuilder.Abstractions;

namespace SqlQueryBuilder.Test.Common.Models.Northwind;

[Table("Categories")]
public record Categories : ISqlEntity
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("CategoryID")]
	public int CategoryID { get; set; }

	[Required]
	[Column("CategoryName")]
	public string CategoryName { get; set; }

	[Column("Description")]
	public string Description { get; set; }

	[Column("Picture")]
	public byte[] Picture { get; set; }
}