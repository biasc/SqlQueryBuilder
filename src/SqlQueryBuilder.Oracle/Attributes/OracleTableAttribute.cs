using System.ComponentModel.DataAnnotations.Schema;

namespace SqlQueryBuilder.Oracle.Attributes;

public class OracleTableAttribute: TableAttribute
{
    public OracleTableAttribute(string name) : base(name)
    {
    }
}