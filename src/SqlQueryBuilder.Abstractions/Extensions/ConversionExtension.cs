﻿using System.Data;

namespace SqlQueryBuilder.Abstractions.Extensions;

public static class ConversionExtension
{
    // public static byte[] Compress(this byte[] obj)
    // {
    //     return default!;
    // }
    //
    // public static byte[] Compress(this string obj)
    // {
    //     return default!;
    // }
    //
    // public static byte[] Decompress(this byte[] obj)
    // {
    //     return default!;
    // }

    public static T Cast<T>(this object obj)
    {
        return default!;
    }

    public static T Cast<T>(this object obj, SqlDbType type)
    {
        return default!;
    }

    public static T Convert<T>(this object obj)
    {
        return default!;
    }

    public static T Convert<T>(this object obj, SqlDbType type)
    {
        return default!;
    }

    public static T Convert<T>(this object obj, SqlDbType type, int style)
    {
        return default!;
    }

    // public static string Unicode(this string obj)
    // {
    //     return default!;
    // }
    //
    // public static string Ascii(this string obj)
    // {
    //     return default!;
    // }
    //
    // public static string Collate(this string obj, string collation)
    // {
    //     return default!;
    // }
}