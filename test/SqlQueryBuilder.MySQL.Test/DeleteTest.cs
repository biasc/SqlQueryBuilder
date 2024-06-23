using NUnit.Framework;
using SqlQueryBuilder.Test.Common;
using SqlQueryBuilder.Test.Common.Models.Delete;

namespace SqlQueryBuilder.MySQL.Test;

[TestFixture]
public class DeleteTest : BaseMySqlTest
{
    [Test]
    public void Delete_Test()
    {
        var compiled = new SqlQueryBuilder()
            .From<DeleteModelEntity>()
            .Delete()
            .Compile();

        compiled.VerifyStatement("DELETE FROM `TestTable`");
        compiled.VerifyEmptyParameters();
    }
    
    [Test]
    public void Delete_Where_1_Clause_Test()
    {
        var compiled = new SqlQueryBuilder()
            .From<DeleteModelEntity>()
            .Where(table=> table.Id > 10)
            .Delete()
            .Compile();

        compiled.VerifyStatement("DELETE FROM `TestTable` WHERE (`ID` > ?p0)");
        compiled.VerifyParametersCount(1);
        compiled.VerifyParameter(0, "?p0", 10);
    }
    
    [Test]
    public void Delete_Custom_Where_1_Clause_Test()
    {
        var compiled = new SqlQueryBuilder()
            .From<DeleteModelCustomEntity>()
            .Where(table=> table.Id > 10)
            .Delete()
            .Compile();
        compiled.VerifyStatement("DELETE FROM `TestTable-MYSQL` WHERE (`ID-MYSQL` > ?p0)");
        compiled.VerifyParametersCount(1);
        compiled.VerifyParameter(0, "?p0", 10);
    }
    
    [Test]
    public void Delete_Where_2_Clause_Test()
    {
        var compiled = new SqlQueryBuilder()
            .From<DeleteModelEntity>()
            .Where(table=> table.Id > 10 && table.Name == "lion")
            .Delete()
            .Compile();

        compiled.VerifyStatement("DELETE FROM `TestTable` WHERE ((`ID` > ?p0) AND (`NAME` = ?p1))");
        compiled.VerifyParametersCount(2);
        compiled.VerifyParameter(0, "?p0", 10);
        compiled.VerifyParameter(1, "?p1", "lion");
    }
}