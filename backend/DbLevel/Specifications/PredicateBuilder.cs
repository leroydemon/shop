using DbLevel.Enum;
using System.Linq.Expressions;
using System.Reflection;

namespace DbLevel.Specifications
{
    public static class PredicateBuilder
    {
        private static readonly MethodInfo ContainsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
        private static readonly MethodInfo StartsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
        private static readonly MethodInfo EndsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });
        private static readonly MethodInfo ToLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);

        public static Expression<Func<T, bool>> Build<T>(string fieldName, ComparisonType comparison, object value)
        {
            var parameter = Expression.Parameter(typeof(T), "t");
            var member = Expression.Property(parameter, fieldName);
            var constant = Expression.Constant(value);
            Expression body = null;

            switch (comparison)
            {
                case ComparisonType.Equal:
                    body = Expression.Equal(member, constant);
                    break;
                case ComparisonType.NotEqual:
                    body = Expression.NotEqual(member, constant);
                    break;
                case ComparisonType.GreaterThan:
                    body = Expression.GreaterThan(member, constant);
                    break;
                case ComparisonType.GreaterThanOrEqual:
                    body = Expression.GreaterThanOrEqual(member, constant);
                    break;
                case ComparisonType.LessThan:
                    body = Expression.LessThan(member, constant);
                    break;
                case ComparisonType.LessThanOrEqual:
                    body = Expression.LessThanOrEqual(member, constant);
                    break;
                case ComparisonType.Contains:
                    body = Expression.Call(member, ContainsMethod, constant);
                    break;
                case ComparisonType.StartsWith:
                    body = Expression.Call(member, StartsWithMethod, constant);
                    break;
                case ComparisonType.EndsWith:
                    body = Expression.Call(member, EndsWithMethod, constant);
                    break;
            }

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        public static Expression<Func<T, bool>> Build<T>(Expression<Func<T, string>> fieldExpression, MethodInfo comparisonMethod, string value)
        {
            var param = Expression.Parameter(typeof(T), "t");
            var body = Expression.Call(Expression.Invoke(fieldExpression, param), comparisonMethod, Expression.Constant(value));
            return Expression.Lambda<Func<T, bool>>(body, param);
        }

        public static Expression<Func<T, bool>> Or<T>(Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));
            var body = Expression.OrElse(Expression.Invoke(expr1, parameter), Expression.Invoke(expr2, parameter));
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        public static Expression<Func<T, bool>> And<T>(Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));
            var body = Expression.AndAlso(Expression.Invoke(expr1, parameter), Expression.Invoke(expr2, parameter));
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}