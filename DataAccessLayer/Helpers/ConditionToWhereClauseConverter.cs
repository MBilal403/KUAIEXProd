using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Helpers
{

    public class ConditionToWhereClauseConverter<T> : ExpressionVisitor
    {
        private StringBuilder whereClause;

        public string ConvertToWhereClause(Expression<Func<T, bool>> condition)
        {
            whereClause = new StringBuilder();
            Visit(condition);
            return whereClause.ToString();
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            whereClause.Append("(");
            Visit(node.Left);

            switch (node.NodeType)
            {
                case ExpressionType.Equal:
                    whereClause.Append(" = ");
                    break;
                case ExpressionType.NotEqual:
                    whereClause.Append(" != ");
                    break;
                case ExpressionType.GreaterThan:
                    whereClause.Append(" > ");
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    whereClause.Append(" >= ");
                    break;
                case ExpressionType.LessThan:
                    whereClause.Append(" < ");
                    break;
                case ExpressionType.LessThanOrEqual:
                    whereClause.Append(" <= ");
                    break;
                case ExpressionType.AndAlso:
                    whereClause.Append(" AND ");
                    break;
                case ExpressionType.OrElse:
                    whereClause.Append(" OR ");
                    break;
                default:
                    throw new NotSupportedException($"The binary operator '{node.NodeType}' is not supported.");
            }

            Visit(node.Right);
            whereClause.Append(")");

            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            var value = node.Value;
            if (value is Guid)
            {
                whereClause.Append($"'{value}'");
            }
            else
            {
                var formattedValue = (value is string) ? $"'{value}'" : value.ToString();
                whereClause.Append(formattedValue);
            }
            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Expression is ConstantExpression constantExpression)
            {
                var value = Expression.Lambda(node).Compile().DynamicInvoke();
                if (value is Guid)
                {
                    whereClause.Append($"'{value}'");
                }
                else
                {
                    var formattedValue = (value is string) ? $"'{value}'" : value.ToString();
                    whereClause.Append(formattedValue);
                }
            }
            else
            {
                whereClause.Append(node.Member.Name);
            }

            return node;
        }

    }

}
