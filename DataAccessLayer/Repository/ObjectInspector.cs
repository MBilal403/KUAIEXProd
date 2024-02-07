using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class ObjectInspector<T> where T : class
    {
        string tableName = typeof(T).Name;
        public string ObjectInspect(params Expression<Func<T, object>>[] columns)
        {
            var data = columns != null && columns.Any() ? string.Join(", ", columns.Select(x => GetColumnName(x))) : "*";
            return data;

        }

        private string GetColumnName(Expression<Func<T, object>> columnExpression)
        {
            // Check if the expression is a MemberExpression
            if (columnExpression.Body is MemberExpression memberExpression)
            {
                // Get the name of the property
                return tableName + "." + memberExpression.Member.Name;
            }
            // Check if the expression is a UnaryExpression with an operand of MemberExpression
            else if (columnExpression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression unaryMemberExpression)
            {
                // Get the name of the property
                return tableName + "." + unaryMemberExpression.Member.Name;
            }
            else
            {
                throw new ArgumentException("Invalid expression. Expression must be a property access expression.");
            }
        }
    }
}
