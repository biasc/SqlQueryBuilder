using NUnit.Framework;
using SqlQueryBuilder.Abstractions.Extensions;
using SqlQueryBuilder.Test.Common;
using SqlQueryBuilder.Test.Common.Models.Select;

namespace SqlQueryBuilder.SqlServer.Test;

[TestFixture]
public class SelectTest : BaseSqlServerTest
{
    [Test]
    public void Select_All_From_Table()
    {
        var compiled = new SqlQueryBuilder()
            .From<SelectModelEntity>()
            .Select(x => x.All())
            .Compile();
        
        compiled.VerifyStatement("SELECT _t0.* FROM [SelectTestTable] AS _t0");
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
        
        compiled.VerifyStatement("SELECT _t0.[ID], _t0.[USERID], _t0.[FIRSTNAME] FROM [SelectTestTable] AS _t0");
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
        
        compiled.VerifyStatement("SELECT _t0.[ID], _t0.[USERID], _t0.[FIRSTNAME] FROM [SelectTestTable] AS _t0 WHERE (_t0.[FIRSTNAME] = @p0)");
        compiled.VerifyParametersCount(1);
        compiled.VerifyParameter(0, "@p0", "user1");
    }
}