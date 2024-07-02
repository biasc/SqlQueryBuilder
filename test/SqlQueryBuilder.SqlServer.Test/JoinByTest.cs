using NUnit.Framework;
using SqlQueryBuilder.Abstractions.Extensions;
using SqlQueryBuilder.Test.Common;
using SqlQueryBuilder.Test.Common.Models.Northwind;

namespace SqlQueryBuilder.SqlServer.Test;

[TestFixture]
public class JoinByTest : BaseSqlServerTest
{
    [Test]
    public void InnerJoin_Test()
    {
        var compiled = new SqlQueryBuilder()
            .From<Products>()
            .InnerJoin<Categories>((p, c) => p.CategoryID == c.CategoryID)
            .Select(x => x.All())
            .Compile();

        var expected = @"SELECT _t0.* 
                         FROM [Products] AS _t0 
                         INNER JOIN [Categories] AS _t1 ON (_t0.[CategoryID] = _t1.[CategoryID])";

        compiled.VerifyStatement(expected);
        compiled.VerifyEmptyParameters();
    }
    
    [Test]
    public void LeftOuterJoin_Test()
    {
        var compiled = new SqlQueryBuilder()
            .From<Products>()
            .LeftOuterJoin<Categories>((p, c) => p.CategoryID == c.CategoryID)
            .Select(x => x.All())
            .Compile();

        var expected = @"SELECT _t0.* 
                         FROM [Products] AS _t0 
                         LEFT OUTER JOIN [Categories] AS _t1 ON (_t0.[CategoryID] = _t1.[CategoryID])";
        
        compiled.VerifyStatement(expected);
        compiled.VerifyEmptyParameters();
    }
    
    [Test]
    public void RightOuterJoin_Test()
    {
        var compiled = new SqlQueryBuilder()
            .From<Products>()
            .RightOuterJoin<Categories>((p, c) => p.CategoryID == c.CategoryID)
            .Select(x => x.All())
            .Compile();

        var expected = @"SELECT _t0.* 
                         FROM [Products] AS _t0 
                         RIGHT OUTER JOIN [Categories] AS _t1 ON (_t0.[CategoryID] = _t1.[CategoryID])";
        
        compiled.VerifyStatement(expected);
        compiled.VerifyEmptyParameters();
    }
    
    [Test]
    public void InnerJoin_Select_Columns_T()
    {
        var compiled = new SqlQueryBuilder()
            .From<Products>()
            .InnerJoin<Categories>((p, c) => p.CategoryID == c.CategoryID)
            .Select(
                p => p.ProductID,
                p => p.ProductName
            )
            .Compile();

        var expected = @"SELECT _t0.[ProductID], _t0.[ProductName]
                         FROM [Products] AS _t0 
                         INNER JOIN [Categories] AS _t1 ON (_t0.[CategoryID] = _t1.[CategoryID])";

        compiled.VerifyStatement(expected);
        compiled.VerifyEmptyParameters();
    }

    [Test]
    public void InnerJoin_Select_Columns_T_TJ1()
    {
        var compiled = new SqlQueryBuilder()
            .From<Products>()
            .InnerJoin<Categories>((p, c) => p.CategoryID == c.CategoryID)
            .Select<CategoryProductProjection>(
                (p, c, projection) => p.ProductID,
                (p, c, projection) => p.ProductName,
                (p, c, projection) => c.CategoryID,
                (p, c, projection) => c.CategoryName
            )
            .Compile();

        var expected = @"SELECT _t0.[ProductID], _t0.[ProductName], _t1.[CategoryID], _t1.[CategoryName]
                      FROM [Products] AS _t0
                      INNER JOIN [Categories] AS _t1 ON (_t0.[CategoryID] = _t1.[CategoryID])";

        compiled.VerifyStatement(expected);
        compiled.VerifyEmptyParameters();
    }
    

   [Test]
   public void Join_With_OrderBy()
   {
    var compiled = new SqlQueryBuilder()
        .From<Products>()
        .InnerJoin<Categories>((p, c) => p.CategoryID == c.CategoryID)
        .OrderBy(
            (p, c) => c.CategoryName.Asc(),
            (p, c) => p.ProductName.Desc()
        )
        .Select(x => x.All())
        .Compile();

    var expected = @"SELECT _t0.*
                      FROM [Products] AS _t0
                      INNER JOIN [Categories] AS _t1 ON (_t0.[CategoryID] = _t1.[CategoryID])
                      ORDER BY 
                      _t1.[CategoryName] ASC, _t0.[ProductName] DESC";

    compiled.VerifyStatement(expected);
    compiled.VerifyEmptyParameters();
   }
   /*
   [Test]
   public void Join_With_OrderBy_And_Select_All()
   {
       var compiled = new SqlQueryBuilder()
           .From<Products>()
           .InnerJoin<Categories>((p, c) => p.CategoryID == c.CategoryID)
           .OrderBy(
               (p, c) => c.CategoryName,
               (p, c) => p.ProductName
           )
           .Select(x => x.All())
           .Compile();

       compiled.VerifyStatement("SELECT _t0.* FROM [SelectTestTable] AS _t0 ORDER BY _t0.[ID] ASC");
       compiled.VerifyEmptyParameters();
   }


   [Test]
   public void Join_With_OrderBy_And_Select_Columns()
   {
   var compiled = new SqlQueryBuilder()
    .From<Products>()
    .InnerJoin<Categories>((p, c) => p.CategoryID == c.CategoryID)
    .OrderBy(
        (p, c) => c.CategoryName,
        (p, c) => p.ProductName
    )
    .Select((p, c) => c.CategoryID,
        (p, c) => c.CategoryName,
        (p, c) => p.ProductID,
        (p, c) => p.ProductName)
    .Compile();

   compiled.VerifyStatement("SELECT _t0.* FROM [SelectTestTable] AS _t0 ORDER BY _t0.[ID] ASC");
   compiled.VerifyEmptyParameters();
   }
   */
}