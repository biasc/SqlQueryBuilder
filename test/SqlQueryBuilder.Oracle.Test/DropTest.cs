using NUnit.Framework;
using SqlQueryBuilder.Abstractions;
using SqlQueryBuilder.Oracle.Test;
using SqlQueryBuilder.Test.Common;
using SqlQueryBuilder.Test.Common.Models.Drop;

namespace SqlQueryBuilder.SqlServer.Test;

[TestFixture]
public class DropTest : BaseOracleTest
{
    [Test]
    public void DropTable_Without_Table_Attribute()
    {
        var compiled = new SqlQueryBuilder()
            .Drop<DropModelEntity>()
            .Compile();

        compiled.VerifyStatement("DROP TABLE \"DropModelEntity\"");
        compiled.VerifyEmptyParameters();
    }

    [Test]
    public void DropTable_With_Table_Attribute()
    {
        var compiled = new SqlQueryBuilder()
            .Drop<DropModelEntityWithTableAttribute>()
            .Compile();

        compiled.VerifyStatement("DROP TABLE \"DropTestTableName\"");
        compiled.VerifyEmptyParameters();
    }
}