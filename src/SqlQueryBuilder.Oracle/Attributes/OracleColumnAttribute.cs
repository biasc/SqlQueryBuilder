using System.ComponentModel.DataAnnotations.Schema;

namespace SqlQueryBuilder.Oracle.Attributes;

public class OracleColumnAttribute: ColumnAttribute
{
    public OracleColumnAttribute(string name) : base(name)
    {
    }
}