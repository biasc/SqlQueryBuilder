using System.ComponentModel.DataAnnotations.Schema;

namespace SqlQueryBuilder.MySQL.Attributes;

public class MySQLTableAttribute: TableAttribute
{
    public MySQLTableAttribute(string name) : base(name)
    {
    }
}