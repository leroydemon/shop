using System.Linq.Expressions;
using System.Reflection;
using DbLevel.Interfaces;
using DbLevel.Models;

namespace DbLevel.Specifications
{
    public abstract class SpecificationBase<T> : SpecificationBase, ISpecification<T>
    {

        public virtual List<Expression<Func<T, bool>>> Criterias { get; } = new();
        public List<Expression<Func<T, object>>> Includes { get; } = new();
        public List<string> IncludeStrings { get; } = new();
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }
        public Expression<Func<T, object>> GroupBy { get; private set; }

        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; set; }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }

        protected void ApplyIncludeList(IEnumerable<string> includes)
        {
            foreach (var include in includes)
            {
                AddInclude(include);
            }
        }

        protected void ApplyIncludeList(IEnumerable<Expression<Func<T, object>>> includes)
        {
            foreach (var include in includes)
            {
                AddInclude(include);
            }
        }

        protected ISpecification<T> ApplyFilterList(IEnumerable<FilterModel> filters)
        {
            foreach (var filter in filters)
            {
                ApplyFilter(PredicateBuilder.Build<T>(filter.FieldName, filter.Comparison, filter.FieldValue));
            }

            return this;
        }

        protected ISpecification<T> ApplyFilter(Expression<Func<T, bool>> expr)
        {
            Criterias.Add(expr);

            return this;
        }

        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }

        protected void ApplyOrderBy(Expression<Func<T, object>> orderByExpression) =>
            OrderBy = orderByExpression;

        protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression) =>
            OrderByDescending = orderByDescendingExpression;

        protected void ApplyGroupBy(Expression<Func<T, object>> groupByExpression) =>
            GroupBy = groupByExpression;

        protected void ApplySearchAll(string searchText, params Expression<Func<T, string>>[] searchFields)
        {
            var searchWords = searchText.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Expression<Func<T, bool>> combinedExpression = t => true;

            foreach (var word in searchWords)
            {
                var wordExpression = searchFields.Select(fieldExpression =>
                {
                    var param = Expression.Parameter(typeof(T), "t");
                    var lowerExpression = Expression.Call(
                        Expression.Invoke(fieldExpression, param),
                        ToLowerMethod
                    );

                    return PredicateBuilder.Build(
                        Expression.Lambda<Func<T, string>>(lowerExpression, param),
                        ContainsMethod,
                        word
                    );
                }).Aggregate(PredicateBuilder.Or);

                combinedExpression = PredicateBuilder.And(combinedExpression, wordExpression);
            }

            ApplyFilter(combinedExpression);
        }

        protected void ApplySearchAny(string searchText, params Expression<Func<T, string>>[] searchFields)
        {
            var searchWords = searchText.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Expression<Func<T, bool>> combinedExpression = t => false;

            foreach (var word in searchWords)
            {
                var wordExpression = searchFields.Select(fieldExpression =>
                {
                    var param = Expression.Parameter(typeof(T), "t");
                    var lowerExpression = Expression.Call(
                        Expression.Invoke(fieldExpression, param),
                        ToLowerMethod
                    );

                    return PredicateBuilder.Build(
                        Expression.Lambda<Func<T, string>>(lowerExpression, param),
                        ContainsMethod,
                        word
                    );
                }).Aggregate(PredicateBuilder.Or);

                combinedExpression = PredicateBuilder.Or(combinedExpression, wordExpression);
            }

            ApplyFilter(combinedExpression);
        }
    }

    public abstract class SpecificationBase
    {
        protected static readonly MethodInfo ToLowerMethod = typeof(string).GetMethod(nameof(string.ToLower), []);
        protected static readonly MethodInfo ContainsMethod = typeof(string).GetMethod(nameof(string.Contains), [typeof(string)]);
    }
}

