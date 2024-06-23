using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using SqlQueryBuilder.Abstractions;
using SqlQueryBuilder.Abstractions.Extensions;
using SqlQueryBuilder.Abstractions.Statements;

namespace SqlQueryBuilder;

public static class SqlQueryCompilerExtension
{
    public static SqlQueryCompiled Compile(this ISqlQuery query)
    {
        var parameterBuilder = new SqlQueryParametersBuilder();
        var tableNameResolver = new SqlQueryTableNameResolver();
        
        var queryExpressionTranslator = new SQLQueryTranslator(parameterBuilder, tableNameResolver);

        var translatedQueryBuilder = new StringBuilder();

        foreach (var queryPart in query)
        {
            var translatedFragment = queryPart.Compute(queryExpressionTranslator);

            if (string.IsNullOrWhiteSpace(translatedFragment))
            {
                continue;
            }

            if (translatedQueryBuilder.Length > 0 && queryPart.PartType != SqlPartType.Batch)
            {
                translatedQueryBuilder.Append($" {Environment.NewLine}");
            }

            translatedQueryBuilder.Append(translatedFragment);
        }

        var translatedQuery = translatedQueryBuilder.ToString();

        var sqlQueryParameters = new ReadOnlyCollection<SqlQueryParameter>(
            parameterBuilder.Parameters.Select(p => p.Value).ToList());

        return new SqlQueryCompiled(translatedQuery, sqlQueryParameters);
    }
}


public interface ISqlQueryParametersBuilder
{
    IDictionary<string, SqlQueryParameter> Parameters { get; }
    string AddParameter(object value);
}

internal class SqlQueryParametersBuilder : ISqlQueryParametersBuilder
{
    public IDictionary<string, SqlQueryParameter> Parameters { get; } = new Dictionary<string, SqlQueryParameter>();

    public string AddParameter(object value)
    {
        var parameterName = $"p{Parameters.Count}";
        parameterName = QueryOptions.ParameterPrefix.ApplyPrefix(parameterName);
        
        Parameters.Add(parameterName, new SqlQueryParameter(parameterName, value));
        return parameterName;
    }

}

    public interface ISqlQueryTableResolver
    {
        IDictionary<Type, QueryExpressionTableName> Alias { get; }

        QueryExpressionTableName ResolveTableName(Type objType);

        string ResolveTableAlias(Type objType);

        string ResolveColumnName(Type objType, MemberInfo member);
    }

    internal class SqlQueryTableNameResolver : ISqlQueryTableResolver
    {
        public IDictionary<Type, QueryExpressionTableName> Alias { get; } = new Dictionary<Type, QueryExpressionTableName>();

        public IDictionary<Type, IDictionary<MemberInfo, string>> MembersTypes { get; } = new Dictionary<Type, IDictionary<MemberInfo, string>>();

        public string ResolveColumnName(Type objType, MemberInfo member)
        {
            if (member?.DeclaringType == null)
                throw new Exception("Declaring type is null");
            
            if (!MembersTypes.TryGetValue(member.DeclaringType, out IDictionary<MemberInfo, string>? members))
            {
                members = new Dictionary<MemberInfo, string>();
                MembersTypes.Add(member.DeclaringType, members);
            }

            if (members.TryGetValue(member, out var columnName)) 
                return columnName;
            
            columnName = QueryOptions.ColumnNameResolver.GetColumnName(member);
            columnName = QueryOptions.ColumnQuoter.Quote(columnName);
            
            members.Add(member, columnName);

            return columnName;
        }


        public QueryExpressionTableName ResolveTableName(Type objType)
        {
            if (Alias.TryGetValue(objType, out QueryExpressionTableName? alias))
                return alias;

            var tableName = QueryOptions.TableNameResolver.GetTableName(objType);
            alias = new QueryExpressionTableName(QueryOptions.TableQuoter.Quote(tableName), $"{QueryOptions.TableAliasPrefix.Prefix}{Alias.Count}" );

            Alias.Add(objType, alias);
            return alias;

        }

        public string ResolveTableAlias(Type objType)
        {
            var tableAlias = ResolveTableName(objType);
            return QueryOptions.TableAliasComposer.ApplyTableAlias(tableAlias.TableName, tableAlias.TableAlias);
        }
    }

    public class QueryExpressionTableName
    {
        public QueryExpressionTableName(string tableName, string tableAlias)
        {
            TableName = tableName;
            TableAlias = tableAlias;
        }

        public string TableName { get; set; }

        public string TableAlias { get; set; }
    }
    

    internal class SQLQueryTranslator : ExpressionVisitor, ISqlQueryTranslator
    {
        private readonly ISqlQueryParametersBuilder _parametersBuilder;
        private readonly ISqlQueryTableResolver _aliasBuilder;
        private readonly StringBuilder _queryBuilder = new StringBuilder();

        public SQLQueryTranslator(
            ISqlQueryParametersBuilder parametersBuilder,
            ISqlQueryTableResolver aliasBuilder)
        {
            _parametersBuilder = parametersBuilder;
            _aliasBuilder = aliasBuilder;
        }

        public string Compute(Expression? expression)
        {
            _queryBuilder.Clear();

            base.Visit(expression);
            return _queryBuilder.ToString();
        }

        public string GetParameterName(object value)
        {
            return _parametersBuilder.AddParameter(value);
        }

        public string GetTableAlias(Type tableType)
        {
            var tableName = _aliasBuilder.ResolveTableName(tableType);
            return tableName.TableAlias;
        }

        public string GetTableName(Type tableType)
        {
            var tableName = _aliasBuilder.ResolveTableName(tableType);
            return tableName.TableName;
        }

        private readonly Regex _matchColumnOnly = new ($"({QueryOptions.TableAliasPrefix.Prefix}[0-9]*\\.*)");
        
        public string GetColumnsWithoutTableAlias(string column)
        {
            var tableAlias = _matchColumnOnly.Match(column).Groups[0].Value;
            return column.Replace(tableAlias, string.Empty);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            _queryBuilder.Append("(");
            Visit(node.Left);

            switch (node.NodeType)
            {
                case ExpressionType.Add:
                    _queryBuilder.Append(" + ");
                    break;

                case ExpressionType.AddAssign:
                    _queryBuilder.Append(" += ");
                    break;

                case ExpressionType.Subtract:
                    _queryBuilder.Append(" - ");
                    break;

                case ExpressionType.SubtractAssign:
                    _queryBuilder.Append(" -= ");
                    break;

                case ExpressionType.And:
                    _queryBuilder.Append(" AND ");
                    break;

                case ExpressionType.AndAlso:
                    _queryBuilder.Append(" AND ");
                    break;

                case ExpressionType.Or:
                    _queryBuilder.Append(" OR ");
                    break;

                case ExpressionType.OrElse:
                    _queryBuilder.Append(" OR ");
                    break;

                case ExpressionType.Equal:
                    if (IsNullConstant(node.Right))
                    {
                        _queryBuilder.Append(" IS ");
                    }
                    else
                    {
                        _queryBuilder.Append(" = ");
                    }
                    break;

                case ExpressionType.NotEqual:
                    if (IsNullConstant(node.Right))
                    {
                        _queryBuilder.Append(" IS NOT ");
                    }
                    else
                    {
                        _queryBuilder.Append(" <> ");
                    }
                    break;

                case ExpressionType.LessThan:
                    _queryBuilder.Append(" < ");
                    break;

                case ExpressionType.LessThanOrEqual:
                    _queryBuilder.Append(" <= ");
                    break;

                case ExpressionType.GreaterThan:
                    _queryBuilder.Append(" > ");
                    break;

                case ExpressionType.GreaterThanOrEqual:
                    _queryBuilder.Append(" >= ");
                    break;

                case ExpressionType.Modulo:
                    _queryBuilder.Append(" % ");
                    break;
                                    
                default:
                    throw new NotSupportedException(string.Format("The binary operator '{0}' is not supported", node.NodeType));

            }

            Visit(node.Right);
            _queryBuilder.Append(")");

            return node;
        }

        protected bool IsNullConstant(Expression exp)
        {
            return (exp.NodeType == ExpressionType.Constant && ((ConstantExpression)exp).Value == null);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var declaringType = node.Method.DeclaringType;
            if (declaringType == typeof(string))
            {
                return VisitStringMethodCall(node);
            }
            
            if (declaringType == typeof(DateTime) || declaringType == typeof(DateTimeOffset))
            {
                return VisitDateTimeMethodCall(node);
            }
            
            if (declaringType == typeof(ConditionExtension))
            {
                return VisitQueryConditionMethodCall(node);
            }
            
            if (declaringType == typeof(SelectionExtension))
            {
                return VisitQuerySelectionMethodCall(node);
            }
            
            if (declaringType == typeof(ConversionExtension))
            {
                return VisitQueryConversionMethodCall(node);
            }
            
            if (declaringType == typeof(DefinitionExtension))
            {
                return VisitQueryDefinitionMethodCall(node);
            }
            
            if (declaringType == typeof(PaginationExtension))
            {
                return VisitQueryPaginationMethodCall(node);
            }
            
            if (declaringType == typeof(OperationExtension))
            {
                return VisitQueryOperationMethodCall(node);
            }
            
            if (declaringType == typeof(AssignmentExtension))
            {
                return VisitQueryAssignmentMethodCall(node);
            }
            
            if (declaringType == typeof(Count))
            {
                return VisitCountMethodCall(node);
            }
            
            if (declaringType == typeof(Row) || declaringType == typeof(IRowNumber))
            {
                return VisitRowMethodCall(node);
            }
            
            if (declaringType == typeof(Case) ||
                declaringType == typeof(ICaseWhen) ||
                (declaringType != null && 
                 declaringType.IsGenericType && 
                 (declaringType.GetGenericTypeDefinition() == typeof(ICase<>) ||
                  declaringType.GetGenericTypeDefinition() == typeof(ICaseThen<>) ||
                  declaringType.GetGenericTypeDefinition() == typeof(ICaseThen<,>) ||
                  declaringType.GetGenericTypeDefinition() == typeof(ICaseWhen<>) ||
                  declaringType.GetGenericTypeDefinition() == typeof(ICaseWhen<,>))))
            {
                return VisitCaseThenElseMethodCall(node);
            }
           
            throw new NotSupportedException(string.Format("The method '{0}' is not supported", node.Method.Name));
        }

        private Expression VisitCountMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == nameof(Count.All))
            {
                _queryBuilder.Append("COUNT(*)");

                return node;
            }

            throw new NotSupportedException(string.Format("The method '{0}' is not supported", node.Method.Name));
        }
        

        private Expression VisitCaseThenElseMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == nameof(Case.When))
            {
                var hasCase = false;
                if (node.Object is MethodCallExpression methodCallExp)
                {
                    Visit(methodCallExp);

                    hasCase = methodCallExp.Method.Name == nameof(ConditionExtension.Case);
                }
                                
                if (!hasCase)
                {
                    int whenCount = 0;
                    CountWhen(node, ref whenCount);

                    hasCase = whenCount > 1;
                }

                _queryBuilder.Append(!hasCase ? "CASE WHEN " : " WHEN ");
                
                var conditions = node.Arguments;
                foreach (var condition in conditions)
                {
                    Visit(condition);
                }

                return node;
            }
            
            if (node.Method.Name == nameof(ICaseWhen.Then))
            {
                if (node.Object is MethodCallExpression methodCallExp)
                {
                    Visit(methodCallExp);
                }

                _queryBuilder.Append(" THEN ");
                Visit(node.Arguments[0]);

                return node;
            }
            
            if (node.Method.Name == nameof(ICaseThen<object>.Else))
            {
                if (node.Object is MethodCallExpression methodCallExp)
                {
                    Visit(methodCallExp.Object);
                    
                    _queryBuilder.Append(" THEN ");
                    Visit(methodCallExp.Arguments[0]);
                    _queryBuilder.Append(" ELSE ");
                    Visit(node.Arguments[0]);
                    _queryBuilder.Append(" END");
                }

                return node;
            }

            throw new NotSupportedException(string.Format("The method '{0}' is not supported", node.Method.Name));
        }

        private void CountWhen(MethodCallExpression node, ref int whenCount)
        {
            if (node.Method.Name == nameof(Case.When))
            {
                whenCount++;
            }

            if (node.Object is MethodCallExpression methodCallExp)
            {
                CountWhen(methodCallExp, ref whenCount);
            }
        }

        private Expression VisitRowMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == nameof(Row.Number))
            {
                _queryBuilder.Append("ROW_NUMBER()");

                return node;
            }
            
            if (node.Method.Name == nameof(IRowNumber.Over))
            {
                Visit(node.Arguments[0]);
                _queryBuilder.Append(" OVER(");
                Visit(node.Arguments[1]);
                _queryBuilder.Append(")");

                return node;
            }

            throw new NotSupportedException(string.Format("The method '{0}' is not supported", node.Method.Name));
        }

        private Expression VisitDateTimeMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == nameof(DateTime.AddYears))
            {
                _queryBuilder.Append("DATEADD(YEAR, ");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(", ");
                Visit(node.Object);
                _queryBuilder.Append(")");

                return node;
            }
            
            if (node.Method.Name == nameof(DateTime.AddMonths))
            {
                _queryBuilder.Append("DATEADD(MONTH, ");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(", ");
                Visit(node.Object);
                _queryBuilder.Append(")");

                return node;
            }
            
            if (node.Method.Name == nameof(DateTime.AddDays))
            {
                _queryBuilder.Append("DATEADD(DAY, ");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(", ");
                Visit(node.Object);
                _queryBuilder.Append(")");

                return node;
            }
            
            if (node.Method.Name == nameof(DateTime.AddHours))
            {
                _queryBuilder.Append("DATEADD(HOUR, ");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(", ");
                Visit(node.Object);
                _queryBuilder.Append(")");

                return node;
            }
            
            if (node.Method.Name == nameof(DateTime.AddMinutes))
            {
                _queryBuilder.Append("DATEADD(MINUTE, ");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(", ");
                Visit(node.Object);
                _queryBuilder.Append(")");

                return node;
            }

            throw new NotSupportedException(string.Format("The method '{0}' is not supported", node.Method.Name));
        }

        private Expression VisitStringMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == nameof(string.StartsWith))
            {
                _queryBuilder.Append("(");
                Visit(node.Object);
                _queryBuilder.Append(" LIKE ");
                var valueExpression = (ConstantExpression)node.Arguments[0];
                var parameterName = _parametersBuilder.AddParameter($"{valueExpression.Value}%");
                _queryBuilder.Append(parameterName);
                _queryBuilder.Append(")");

                return node;
            }
            
            if (node.Method.Name == nameof(string.EndsWith))
            {
                _queryBuilder.Append("(");
                Visit(node.Object);
                _queryBuilder.Append(" LIKE ");
                var valueExpression = (ConstantExpression)node.Arguments[0];
                var parameterName = _parametersBuilder.AddParameter($"%{valueExpression.Value}");
                _queryBuilder.Append(parameterName);
                _queryBuilder.Append(")");

                return node;
            }
            
            if (node.Method.Name == nameof(string.Contains))
            {
                _queryBuilder.Append("(");
                Visit(node.Object);
                _queryBuilder.Append(" LIKE ");
                var valueExpression = (ConstantExpression)node.Arguments[0];
                var parameterName = _parametersBuilder.AddParameter($"%{valueExpression.Value}%");
                _queryBuilder.Append(parameterName);
                _queryBuilder.Append(")");

                return node;
            }
            
            if (node.Method.Name == nameof(string.Substring))
            {
                if (node.Arguments.Count == 1)
                {
                    _queryBuilder.Append("LEFT(");
                    Visit(node.Object);
                    _queryBuilder.Append(", ");
                    Visit(node.Arguments[0]);
                    _queryBuilder.Append(")");
                }
                else
                {
                    _queryBuilder.Append("SUBSTRING(");
                    Visit(node.Object);
                    _queryBuilder.Append(", ");
                    Visit(node.Arguments[0]);
                    _queryBuilder.Append(", ");
                    Visit(node.Arguments[1]);
                    _queryBuilder.Append(")");
                }

                return node;
            }
            
            if (node.Method.Name == nameof(string.Trim))
            {
                _queryBuilder.Append("TRIM(");
                Visit(node.Object);
                _queryBuilder.Append(")");

                return node;
            }
            
            if (node.Method.Name == nameof(string.TrimStart))
            {
                _queryBuilder.Append("LTRIM(");
                Visit(node.Object);
                _queryBuilder.Append(")");

                return node;
            }
            
            if (node.Method.Name == nameof(string.TrimEnd))
            {
                _queryBuilder.Append("RTRIM(");
                Visit(node.Object);
                _queryBuilder.Append(")");

                return node;
            }
            
            if (node.Method.Name == nameof(string.ToUpper))
            {
                _queryBuilder.Append("UPPER(");
                Visit(node.Object);
                _queryBuilder.Append(")");

                return node;
            }
            
            if (node.Method.Name == nameof(string.ToLower))
            {
                _queryBuilder.Append("LOWER(");
                Visit(node.Object);
                _queryBuilder.Append(")");

                return node;
            }
            
            if (node.Method.Name == nameof(string.Replace))
            {
                _queryBuilder.Append("REPLACE(");
                Visit(node.Object);
                _queryBuilder.Append(", ");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(", ");
                Visit(node.Arguments[1]);
                _queryBuilder.Append(")");

                return node;
            }
            
            if (node.Method.Name == nameof(string.IndexOf))
            {
                _queryBuilder.Append("CHARINDEX(");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(", ");
                Visit(node.Object);
                _queryBuilder.Append(")");

                return node;
            }
            
            if (node.Method.Name == nameof(string.Concat))
            {
                _queryBuilder.Append("CONCAT(");
                var idx = node.Arguments.Count - 1;
                foreach (var arg in node.Arguments)
                {
                    Visit(arg);

                    if (idx-- > 0)
                    {
                        _queryBuilder.Append(", ");
                    }
                }
                _queryBuilder.Append(")");

                return node;
            }

            throw new NotSupportedException(string.Format("The method '{0}' is not supported", node.Method.Name));
        }

        public Expression VisitQueryPaginationMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == nameof(PaginationExtension.Offset))
            {
                _queryBuilder.Append("OFFSET ");
                Visit(node.Arguments[1]);
                _queryBuilder.Append(" ROWS");

                return node;
            }
            
            if (node.Method.Name == nameof(PaginationExtension.Fetch))
            {
                if (node.Arguments[0] is MethodCallExpression)
                {
                    Visit(node.Arguments[0]);
                    _queryBuilder.Append(" FETCH NEXT ");
                    Visit(node.Arguments[1]);
                }
                else
                {
                    _queryBuilder.Append("FETCH FIRST ");
                    Visit(node.Arguments[1]);
                }

                _queryBuilder.Append(" ROWS ONLY");

                return node;
            }
            
            if (node.Method.Name == nameof(PaginationExtension.PartitionBy))
            {
                Visit(node.Arguments[0]);
                _queryBuilder.Append("PARTITION BY ");
                Visit(node.Arguments[1]);
                _queryBuilder.Append(" ");

                return node;
            }
            
            if (node.Method.Name == nameof(PaginationExtension.OrderBy))
            {
                Visit(node.Arguments[0]);
                _queryBuilder.Append("ORDER BY ");
                Visit(node.Arguments[1]);

                return node;
            }
            
            if (node.Method.Name == nameof(PaginationExtension.Over))
            {
                Visit(node.Arguments[0]);
                _queryBuilder.Append(" OVER(");
                Visit(node.Arguments[1]);
                _queryBuilder.Append(")");

                return node;
            }

            throw new NotSupportedException(string.Format("The method '{0}' is not supported", node.Method.Name));
        }

        private Expression VisitQuerySelectionMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == nameof(SelectionExtension.All))
            {
                var parameterExp = (ParameterExpression)node.Arguments[0];
                var alias = _aliasBuilder.ResolveTableName(parameterExp.Type);
                _queryBuilder.Append($"{alias.TableAlias}.*");

                return node;
            }
            
            if (node.Method.Name == nameof(SelectionExtension.As))
            {
                Visit(node.Arguments[0]);
                _queryBuilder.Append(" AS ");
                if (node.Arguments[1] is MemberExpression memberExp)
                {
                    _queryBuilder.Append(_aliasBuilder.ResolveColumnName(memberExp.Type, memberExp.Member));
                }
                else
                {
                    var unaryExp = (UnaryExpression)node.Arguments[1];
                    var opMemberExp = (MemberExpression)unaryExp.Operand;
                    _queryBuilder.Append(_aliasBuilder.ResolveColumnName(opMemberExp.Type, opMemberExp.Member));
                }

                return node;
            }
            
            if (node.Method.Name == nameof(SelectionExtension.Inserted))
            {
                _queryBuilder.Append($"INSERTED.");
                if (node.Arguments[0] is MethodCallExpression methodCallExp && methodCallExp.Method.Name == nameof(SelectionExtension.All))
                {
                    _queryBuilder.Append($"*");

                    return node;
                }
                
                if (node.Arguments[0] is MemberExpression memberxp)
                {
                    _queryBuilder.Append(_aliasBuilder.ResolveColumnName(memberxp.Type, memberxp.Member));

                    return node;
                }
            }
            else if (node.Method.Name == nameof(SelectionExtension.Deleted))
            {
                _queryBuilder.Append($"DELETED.");
                if (node.Arguments[0] is MethodCallExpression methodCallExp && methodCallExp.Method.Name == nameof(SelectionExtension.All))
                {
                    _queryBuilder.Append($"*");

                    return node;
                }
                
                if (node.Arguments[0] is MemberExpression memberxp)
                {
                    _queryBuilder.Append(_aliasBuilder.ResolveColumnName(memberxp.Type, memberxp.Member));

                    return node;
                }
            }

            throw new NotSupportedException(string.Format("The method '{0}' is not supported", node.Method.Name));
        }

        private Expression VisitQueryConversionMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == nameof(ConversionExtension.Cast))
            {
                _queryBuilder.Append("CAST(");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(" AS ");
                if (node.Arguments.Count == 2)
                {
                    Visit(node.Arguments[1]);
                }
                else
                {
                    Visit(GetConstantExpressionOfDbType(node.Method.ReturnType));
                }
                _queryBuilder.Append(")");

                return node;
            }
            
            if (node.Method.Name == nameof(ConversionExtension.Convert))
            {
                _queryBuilder.Append("CONVERT(");
                if (node.Arguments.Count == 3)
                {
                    Visit(node.Arguments[1]);
                    _queryBuilder.Append(", ");
                    Visit(node.Arguments[0]);
                    _queryBuilder.Append($", {((ConstantExpression)node.Arguments[2]).Value}");
                }
                else if (node.Arguments.Count == 2)
                {
                    Visit(node.Arguments[1]);
                    _queryBuilder.Append(", ");
                    Visit(node.Arguments[0]);
                }
                else
                {
                    Visit(GetConstantExpressionOfDbType(node.Method.ReturnType));
                    _queryBuilder.Append(", ");
                    Visit(node.Arguments[0]);
                }
                _queryBuilder.Append(")");

                return node;
            }

            throw new NotSupportedException(string.Format("The method '{0}' is not supported", node.Method.Name));
        }

        private Expression GetConstantExpressionOfDbType(Type returnType)
        {
            SqlDbType? sqlDbType = null;

            if (returnType == typeof(Int16))
            {
                sqlDbType = SqlDbType.SmallInt;
            }
            else if (returnType == typeof(Int32))
            {
                sqlDbType = SqlDbType.Int;
            }
            else if (returnType == typeof(Int64))
            {
                sqlDbType = SqlDbType.BigInt;
            }
            else if (returnType == typeof(DateTime))
            {
                sqlDbType = SqlDbType.DateTime2;
            }
            else if (returnType == typeof(DateTimeOffset))
            {
                sqlDbType = SqlDbType.DateTimeOffset;
            }
            else if (returnType == typeof(double))
            {
                sqlDbType = SqlDbType.Decimal;
            }
            else if (returnType == typeof(TimeSpan))
            {
                sqlDbType = SqlDbType.Time;
            }
            
            if (sqlDbType.HasValue)
            {
                return Expression.Constant(sqlDbType.Value);
            }

            throw new NotSupportedException(string.Format("The implicit conversion in SqlDbType of '{0}' is not supported", returnType.Name));
        }

        private Expression VisitQueryDefinitionMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == nameof(DefinitionExtension.Max))
            {
                Visit(node.Arguments[0]);
                _queryBuilder.Append("(MAX)");

                return node;
            }
            
            if (node.Method.Name == nameof(DefinitionExtension.Size))
            {
                Visit(node.Arguments[0]);
                _queryBuilder.Append($"({((ConstantExpression)node.Arguments[1]).Value})");
                
                return node;
            }
            if (node.Method.Name == nameof(DefinitionExtension.Asc))
            {
                Visit(node.Arguments[0]);
                _queryBuilder.Append(" ASC");

                return node;
            }
            
            if (node.Method.Name == nameof(DefinitionExtension.Desc))
            {
                Visit(node.Arguments[0]);
                _queryBuilder.Append(" DESC");

                return node;
            }

            throw new NotSupportedException(string.Format("The method '{0}' is not supported", node.Method.Name));
        }

        private Expression VisitQueryConditionMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == nameof(ConditionExtension.IsNull))
            {
                if (node.Arguments.Count == 1)
                {
                    _queryBuilder.Append("(");
                    Visit(node.Arguments[0]);
                    _queryBuilder.Append(" IS NULL");
                    _queryBuilder.Append(")");
                }
                else
                {
                    _queryBuilder.Append("ISNULL(");
                    Visit(node.Arguments[0]);
                    _queryBuilder.Append(", ");
                    Visit(node.Arguments[1]);
                    _queryBuilder.Append(")");
                }

                return node;
            }
            
            if (node.Method.Name == nameof(ConditionExtension.IsNotNull))
            {
                _queryBuilder.Append("(");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(" IS NOT NULL");
                _queryBuilder.Append(")");

                return node;
            }
            
            if (node.Method.Name == nameof(ConditionExtension.In))
            {
                _queryBuilder.Append("(");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(" IN (");
                Visit(node.Arguments[1]);
                _queryBuilder.Append("))");

                return node;
            }
            
            if (node.Method.Name == nameof(ConditionExtension.NotIn))
            {
                _queryBuilder.Append("(");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(" NOT IN (");
                Visit(node.Arguments[1]);
                _queryBuilder.Append("))");

                return node;
            }
            
            if (node.Method.Name == nameof(ConditionExtension.Case))
            {
                _queryBuilder.Append("CASE ");
                Visit(node.Arguments[0]);

                return node;
            }

            throw new NotSupportedException(string.Format("The method '{0}' is not supported", node.Method.Name));
        }

        private Expression VisitQueryOperationMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == nameof(OperationExtension.Sum))
            {
                _queryBuilder.Append("SUM(");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(")");

                return node;
            }
            
            if (node.Method.Name == nameof(OperationExtension.Sign))
            {
                _queryBuilder.Append("SIGN(");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(")");

                return node;
            }
            
            if (node.Method.Name == nameof(OperationExtension.Count))
            {
                if (!node.Arguments[0].Type.IsClass)
                {
                    _queryBuilder.Append("COUNT(");
                    Visit(node.Arguments[0]);
                    _queryBuilder.Append(")");

                    return node;
                }
            }
            else if (node.Method.Name == nameof(OperationExtension.CountDistinct))
            {
                if (!node.Arguments[0].Type.IsClass)
                {
                    _queryBuilder.Append("COUNT(DISTINCT ");
                    Visit(node.Arguments[0]);
                    _queryBuilder.Append(")");

                    return node;
                }
            }
            else if (node.Method.Name == nameof(OperationExtension.Max))
            {
                _queryBuilder.Append("MAX(");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(OperationExtension.Min))
            {
                _queryBuilder.Append("MIN(");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(OperationExtension.Left))
            {
                _queryBuilder.Append("LEFT(");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(", ");
                Visit(node.Arguments[1]);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(OperationExtension.Right))
            {
                _queryBuilder.Append("RIGHT(");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(", ");
                Visit(node.Arguments[1]);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(OperationExtension.Len))
            {
                _queryBuilder.Append("LEN(");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(")");

                return node;
            }
            
            throw new NotSupportedException(string.Format("The method '{0}' is not supported", node.Method.Name));
        }

        private Expression VisitQueryAssignmentMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == nameof(AssignmentExtension.Set))
            {
                Visit(node.Arguments[0]);
                _queryBuilder.Append(" = ");
                Visit(node.Arguments[1]);

                return node;
            }

            throw new NotSupportedException(string.Format("The method '{0}' is not supported", node.Method.Name));
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Expression != null && node.Expression.NodeType == ExpressionType.Parameter)
            {
                var tableName = _aliasBuilder.ResolveTableName(node.Expression.Type);
                var columnName = _aliasBuilder.ResolveColumnName(node.Expression.Type, node.Member);
                
                _queryBuilder.Append($"{tableName.TableAlias}.{columnName}");
                return node;
            }
            
            if (node.Expression != null && node.Expression.NodeType == ExpressionType.Constant)
            {                
                var container = ((ConstantExpression)node.Expression).Value;
                var member = node.Member;
                if (member is FieldInfo field)
                {
                    var value = field.GetValue(container);
                    Visit(Expression.Constant(value));
                }
                else if (member is PropertyInfo propery)
                {
                    var value = propery.GetValue(container, null);
                    Visit(Expression.Constant(value));
                }
                else
                {
                    Visit(node.Expression);
                }
                
                return node;
            }
            
            if (node.NodeType == ExpressionType.MemberAccess)
            {
                if (node.Expression == null)
                {
                    object value = GetMemberValue(node);

                    Visit(Expression.Constant(value));

                    return node;
                }
                
                if (node.Expression is MethodCallExpression methodCall &&
                    methodCall.Method.Name == nameof(DateTime.Subtract)) 
                {
                    _queryBuilder.Append("DATEDIFF(");
                    var memberName = node.Member.Name.ToUpper().Replace("TOTAL", string.Empty);
                    memberName = memberName.Substring(0, memberName.Length - 1);
                    _queryBuilder.Append($"{memberName}, ");
                    Visit(methodCall.Arguments[0]);
                    _queryBuilder.Append(", ");
                    Visit(methodCall.Object);
                    _queryBuilder.Append(")");

                    return node;
                }
                
                if (node.Expression != null && node.Expression.NodeType == ExpressionType.MemberAccess)
                {
                    var memberExp = (MemberExpression)node.Expression;
                    if (memberExp.Expression != null && memberExp.Expression.NodeType == ExpressionType.Constant)
                    {
                        object?container = ((ConstantExpression)memberExp.Expression).Value;
                        if (container != null)
                        {
                            var containerTypeName = container.GetType().Name;
                            if (IsCSharpGeneratedClass(containerTypeName, "DisplayClass") ||
                                IsCSharpGeneratedClass(containerTypeName, "AnonymousType"))
                            {
                                var displayClassMember = memberExp.Member;
                                if (displayClassMember is FieldInfo fieldDisplayClassMember)
                                {
                                    container = fieldDisplayClassMember.GetValue(container);
                                }
                                else if (displayClassMember is PropertyInfo propertyDisplayClassMember)
                                {
                                    container = propertyDisplayClassMember.GetValue(container, null);
                                }
                            }

                            var member = node.Member;
                            if (member is FieldInfo field)
                            {
                                object? value = field.GetValue(container);
                                Visit(Expression.Constant(value));
                            }
                            else if (member is PropertyInfo propery)
                            {
                                object? value = propery.GetValue(container, null);
                                Visit(Expression.Constant(value));
                            }
                            else
                            {
                                Visit(node.Expression);
                            }
                        }

                        return node;
                    }
                }
            }


            throw new NotSupportedException(string.Format("The member '{0}' is not supported", node.Member.Name));
        }

        private static bool IsCSharpGeneratedClass(string typeName, string pattern)
        {
            return typeName.Contains("<>") && typeName.Contains("__") && typeName.Contains(pattern);
        }

        private static object GetMemberValue(MemberExpression node)
        {
            var objectMember = Expression.Convert(node, typeof(object));

            var getterLambda = Expression.Lambda<Func<object>>(objectMember);

            var getter = getterLambda.Compile();

            var value = getter();

            return value;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (node.Value == null)
            {
                _queryBuilder.Append("NULL");
            }
            else if (node.Value is Type valueType)
            {
                var alias = _aliasBuilder.ResolveTableAlias(valueType);
                _queryBuilder.Append(alias);
            }
            else if (node.Value is SqlDbType dbType)
            {
                _queryBuilder.Append(dbType.ToString().ToUpper());
            }
            else if (node.Value is byte[])
            {
                string parameterName = _parametersBuilder.AddParameter(node.Value);
                _queryBuilder.Append(parameterName);
            }
            else if (node.Value is IList listValues)
            {                
                var parameters = new List<string>();
                foreach (var v in listValues)
                {
                    string parameterName = _parametersBuilder.AddParameter(v);
                    parameters.Add(parameterName);
                }

                _queryBuilder.Append(string.Join(", ", parameters));
            }
            else
            {
                string parameterName = _parametersBuilder.AddParameter(node.Value);
                _queryBuilder.Append(parameterName);
            }

            return node;
        }

        protected override Expression VisitListInit(ListInitExpression node)
        {
            var valuesExp = node.Initializers.SelectMany(i => i.Arguments);
            
            var listVal = new List<object?>();

            foreach (var vExp in valuesExp)
            {
                var val = (ConstantExpression)vExp;
                listVal.Add(val.Value);
            }
            
            Visit(Expression.Constant(listVal));

            return node;
        }

        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            var idx = node.Expressions.Count - 1;
            foreach (var exp in node.Expressions)
            {
                Visit(exp);

                if (idx-- > 0)
                {
                    _queryBuilder.Append(", ");
                }
            }
            
            return node;
        }
    }