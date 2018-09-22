using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace UniversityAPI.Helpers
{
    public class RepositoryHelper<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputCollection"></param>
        /// <param name="filterValue"></param>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        public static IQueryable<T> FilterCollectionOverString(IQueryable<T> inputCollection, string filterValue, Expression<Func<T, bool>> filterExpression)
        {
            if (!string.IsNullOrWhiteSpace(filterValue))
            {
                inputCollection = inputCollection.Where(filterExpression);
            }

            return inputCollection;
        }
    }
}
