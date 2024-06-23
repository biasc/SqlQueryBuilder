using System.ComponentModel.DataAnnotations.Schema;

namespace SqlQueryBuilder.MySQL.Attributes;

public class MySQLColumnAttribute: ColumnAttribute
{
    public MySQLColumnAttribute(string name) : base(name)
    {
    }
}