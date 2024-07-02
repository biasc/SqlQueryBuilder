using NUnit.Framework;
using SqlQueryBuilder.Test.Common;
using SqlQueryBuilder.Test.Common.Models;

namespace SqlQueryBuilder.MySQL.Test;

[TestFixture]
public class DropTest : BaseMySqlTest
{
    [Test]
    public void DropTable_Without_Table_Attribute()
    {
        var compiled = new SqlQueryBuilder()
            .Drop<DropModelEntity>()
            .Compile();

        compiled.VerifyStatement("DROP TABLE `DropModelEntity`");
        compiled.VerifyEmptyParameters();
    }

    [Test]
    public void DropTable_With_Table_Attribute()
    {
        var compiled = new SqlQueryBuilder()
            .Drop<DropModelEntityWithTableAttribute>()
            .Compile();

        compiled.VerifyStatement("DROP TABLE `DropTestTableName`");
        compiled.VerifyEmptyParameters();
    }
}