using System.ComponentModel.DataAnnotations.Schema;
using SqlQueryBuilder.Abstractions;
using SqlQueryBuilder.MySQL.Attributes;
using SqlQueryBuilder.Oracle.Attributes;
using SqlQueryBuilder.SqlServer.Attributes;

namespace SqlQueryBuilder.Test.Common.Models.Delete;

[OracleTable("TestTable-ORACLE")]
[SqlServerTable("TestTable-SQL")]
[MySQLTable("TestTable-MYSQL")]
[Table("TestTable")]
public class DeleteModelCustomEntity: ISqlEntity
{
    [OracleColumn("ID-ORACLE")]
    [SqlServerColumn("ID-SQL")]
    [MySQLColumn("ID-MYSQL")]
    [Column("ID")]
    public int Id { get; set; }
    
    [OracleColumn("NAME-ORACLE")]
    [SqlServerColumn("NAME-SQL")]
    [MySQLColumn("NAME-MYSQL")]
    [Column("NAME")]
    public string? Name { get; set; }
}