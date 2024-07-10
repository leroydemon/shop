using System.Linq.Expressions;

namespace DbLevel
{
    //я скину файлы с спецификациями готовыми, это можно будет удалить
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            var parameterMap = left.Parameters
                .Select((f, i) => new { f, s = right.Parameters[i] })
                .ToDictionary(p => p.s, p => p.f);

            var secondBody = ParameterRebinder.ReplaceParameters(parameterMap, right.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left.Body, secondBody), left.Parameters);
        }
    }
}
