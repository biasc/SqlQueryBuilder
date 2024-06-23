namespace SqlQueryBuilder.Abstractions.Extensions;

public static class AssignmentExtension
{
    public static void Set<T>(this T obj, T value) where T : struct
    {
        
    }

    public static void Set(this byte[] obj, byte[] value)
    {

    }

    public static void Set<T>(this T? obj, T? value) where T : struct
    {

    }

    public static void Set(this string obj, string value)
    {

    }
}