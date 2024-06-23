using System.Text.RegularExpressions;
using NUnit.Framework;

namespace SqlQueryBuilder.Test.Common;

public static class CompareSqlQueryExtension
{
    private static string GetCleanSql(
        this SqlQueryCompiled compiled)
    {
        var cleanText = compiled.Statement.GetCleanSql();

        return cleanText;
    }
    
    private static string GetCleanSql(
        this string statement)
    {
        var cleanText = statement
            .Replace(Environment.NewLine, string.Empty);

        cleanText = Regex.Replace(cleanText, @"\s+", " ");
        
        return cleanText;
    }

    public static void VerifyStatement(
        this SqlQueryCompiled compiled,
        string expected)
    {
        var cleanValue = compiled.GetCleanSql();
        var cleanExpected = expected.GetCleanSql();
        
        Assert.That(cleanValue, Is.EqualTo(cleanExpected));
    }

    public static void VerifyEmptyParameters(
        this SqlQueryCompiled compiled)
    {
        Assert.That(compiled.Parameters, Is.Empty);
    }
    public static void VerifyParametersCount(
        this SqlQueryCompiled compiled,
        int expectedCount)
    {
        Assert.That(compiled.Parameters.Count, Is.EqualTo(expectedCount));
    }

    public static void VerifyParameter(
        this SqlQueryCompiled compiled,
        int index,
        string name,
        object value)
    {
        var parameter = compiled.Parameters[index];
        
        Assert.That(parameter.Name, Is.EqualTo(name));
        Assert.That(parameter.Value, Is.EqualTo(value));
    }
}