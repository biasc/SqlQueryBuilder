﻿using System.Linq.Expressions;

namespace SqlQueryBuilder.Abstractions.Statements;

public static class Row
{
    public static int Number()
    {
        return default;
    }
}

public interface IRowNumber
{
    int Over(Expression<Func<IRowNumberOver, object>> expression);
}

public interface IRowNumberOver
{

}