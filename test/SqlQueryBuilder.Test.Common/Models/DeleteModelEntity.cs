using System.ComponentModel.DataAnnotations.Schema;
using SqlQueryBuilder.Abstractions;

namespace SqlQueryBuilder.Test.Common.Models;

[Table("TestTable")]
public class DeleteModelEntity: ISqlEntity
{
    [Column("ID")]
    public int Id { get; set; }
    
    [Column("NAME")]
    public string? Name { get; set; }
}