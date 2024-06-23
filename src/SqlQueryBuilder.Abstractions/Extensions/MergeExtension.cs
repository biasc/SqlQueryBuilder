namespace SqlQueryBuilder.Abstractions.Extensions;

public static class MergeExtension
{
    public static T[] Merge<T>(this T first, params T[]? others)
    {
        var list = new List<T> { first };

        if (others != null && others.Length > 0)
        {
            list.AddRange(others);
        }

        return list.ToArray();
    }
}