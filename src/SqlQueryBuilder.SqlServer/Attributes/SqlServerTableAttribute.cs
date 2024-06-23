using System.ComponentModel.DataAnnotations.Schema;

namespace SqlQueryBuilder.SqlServer.Attributes;

public class SqlServerTableAttribute: TableAttribute
{
    public SqlServerTableAttribute(string name) : base(name)
    {
    }
}