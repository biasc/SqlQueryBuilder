using System.ComponentModel.DataAnnotations.Schema;

namespace SqlQueryBuilder.SqlServer.Attributes;

public class SqlServerColumnAttribute: ColumnAttribute
{
    public SqlServerColumnAttribute(string name) : base(name)
    {
    }
}