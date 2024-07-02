using NUnit.Framework;
using SqlQueryBuilder.Abstractions.Extensions;
using SqlQueryBuilder.Test.Common;
using SqlQueryBuilder.Test.Common.Models;

namespace SqlQueryBuilder.Oracle.Test;

[TestFixture]
public class OrderByTest : BaseOracleTest
{
    [Test]
    public void Select_All_From_Table_Order_By_Asc()
    {
        var compiled = new SqlQueryBuilder()
            .From<SelectModelEntity>()
            .OrderBy(x => x.Id.Asc())
            .Select(x => x.All())
            .Compile();

        compiled.VerifyStatement("SELECT tAlias0.* FROM \"SelectTestTable\" tAlias0 ORDER BY tAlias0.\"ID\" ASC");
        compiled.VerifyEmptyParameters();
    }
    
    [Test]
    public void Select_All_From_Table_Order_By_Desc()
    {
        var compiled = new SqlQueryBuilder()
            .From<SelectModelEntity>()
            .OrderBy(x => x.Id.Desc())
            .Select(x => x.All())
            .Compile();

        compiled.VerifyStatement("SELECT tAlias0.* FROM \"SelectTestTable\" tAlias0 ORDER BY tAlias0.\"ID\" DESC");
        compiled.VerifyEmptyParameters();
    }
}