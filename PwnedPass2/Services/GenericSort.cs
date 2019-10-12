using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PwnedPass2.Services
{
    public class GenericSort<T>
    {
        public IEnumerable<T> Sort(IEnumerable<T> source, string sortBy, bool sortDirection)
        {
            var param = Expression.Parameter(typeof(T), "item");

            var sortExpression = Expression.Lambda<Func<T, object>>
                (Expression.Convert(Expression.Property(param, sortBy), typeof(object)), param);

            switch (sortDirection)
            {
                case true:
                    return source.AsQueryable<T>().OrderBy<T, object>(sortExpression);

                default:
                    return source.AsQueryable<T>().OrderByDescending<T, object>(sortExpression);
            }
        }
    }
}