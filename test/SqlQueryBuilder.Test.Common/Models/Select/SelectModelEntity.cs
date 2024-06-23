using System.ComponentModel.DataAnnotations.Schema;
using SqlQueryBuilder.Abstractions;

namespace SqlQueryBuilder.Test.Common.Models.Select;

[Table("SelectTestTable")]
public class SelectModelEntity: ISqlEntity
{
    [Column("ID")]
    public int Id { get; set; }
    
    [Column("USERID")]
    public int UserId { get; set; }
    
    [Column("EMAIL")]
    public string? Email { get; set; }
    
    [Column("FIRSTNAME")]
    public string? FirstName { get; set; }
}