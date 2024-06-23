using NUnit.Framework;
using SqlQueryBuilder.Abstractions.Extensions;
using SqlQueryBuilder.Test.Common;
using SqlQueryBuilder.Test.Common.Models.Select;

namespace SqlQueryBuilder.Oracle.Test;

[TestFixture]
public class SelectTest : BaseOracleTest
{
    [Test]
    public void Select_All_From_Table()
    {
        var compiled = new SqlQueryBuilder()
            .From<SelectModelEntity>()
            .Select(x => x.All())
            .Compile();
        
        compiled.VerifyStatement("SELECT tAlias0.* FROM \"SelectTestTable\" tAlias0");
        compiled.VerifyEmptyParameters();
    }
    
    [Test]
    public void Select_Specific_Fields_From_Table()
    {
        var compiled = new SqlQueryBuilder()
            .From<SelectModelEntity>()
            .Select(
                x => x.Id,
                x => x.UserId,
                x => x.FirstName)
            .Compile();
        
        compiled.VerifyStatement("SELECT tAlias0.\"ID\", tAlias0.\"USERID\", tAlias0.\"FIRSTNAME\" FROM \"SelectTestTable\" tAlias0");
        compiled.VerifyEmptyParameters();
    }
    
    [Test]
    public void Select_Specific_Fields_From_Table_With_Filter()
    {
        var compiled = new SqlQueryBuilder()
            .From<SelectModelEntity>()
            .Where(x => x.FirstName == "user1")
            .Select(
                x => x.Id,
                x => x.UserId,
                x => x.FirstName)
            .Compile();
        
        compiled.VerifyStatement("SELECT tAlias0.\"ID\", tAlias0.\"USERID\", tAlias0.\"FIRSTNAME\" FROM \"SelectTestTable\" tAlias0 WHERE (tAlias0.\"FIRSTNAME\" = :p0)");
        compiled.VerifyParametersCount(1);
        compiled.VerifyParameter(0, ":p0", "user1");
    }
}