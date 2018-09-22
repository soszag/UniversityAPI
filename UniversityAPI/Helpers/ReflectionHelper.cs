using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAPI.Helpers
{
    public class ReflectionHelper
    {
        /// <summary>
        /// Peform filtering operation over string property. Returns modified collection after applying filtering.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        /// <param name="currentCollection"></param>
        /// <param name="caseSensitive"></param>
        /// <returns></returns>
        public static IQueryable<T> PerformFilterOverString<T>(string fieldName, string value, IQueryable<T> currentCollection, bool caseSensitive = false)
        {
            if (!string.IsNullOrWhiteSpace(fieldName))
            {
                string filter = (caseSensitive ? fieldName.ToLowerInvariant() : fieldName).Trim();
                return currentCollection.Where(st => CheckIfObjectHasPropertyContaingValue(st, fieldName, value, caseSensitive ));
            }
            return currentCollection;
        }

        /// <summary>
        /// Method perform sorting operations by given parameters over collection and returns sorted collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static IQueryable<T> PerformSorting<T>(string parameters, IQueryable<T> collection)
        {
            var splitted = parameters.Split(',');
            bool isFirstSortParam = true;

            foreach (var par in splitted)
            {
                var paramData = par.Split(' ');
                
                if (isFirstSortParam)
                {
                    if (paramData.Length == 2 && paramData[1] == "desc")
                    {
                        collection = collection.OrderByDescending(o => GetSelector<T>(o, paramData[0]));
                    }
                    else
                    {
                        collection = collection.OrderBy(o => GetSelector<T>(o, paramData[0]));
                    }
                    
                    isFirstSortParam = false;
                }
                else
                {
                    if (paramData.Length == 2 && paramData[1] == "desc")
                    {
                        collection = (collection as IOrderedQueryable<T>).ThenByDescending(o => GetSelector<T>(o, paramData[0]));
                    }
                    else
                    {
                        collection = (collection as IOrderedQueryable<T>).ThenBy(o => GetSelector<T>(o, paramData[0]));
                    }
                }
            }

            return collection;
        }

        /// <summary>
        /// Return value of property defined by 'fieldName' from object 'obj'
        /// </summary>
        /// <typeparam name="T">Type of object to return value of property</typeparam>
        /// <param name="obj">Reference to object to retrieve value from</param>
        /// <param name="fieldName">Name of property to return value</param>
        /// <returns></returns>
        private static object GetSelector<T>(T obj, string fieldName)
        {
            var type = obj.GetType();
            var prop = type.GetProperties()
                      .Where(pr => string.Equals(pr.Name, fieldName, StringComparison.OrdinalIgnoreCase))
                      .FirstOrDefault();

            return prop?.GetValue(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propName"></param>
        /// <param name="propValue"></param>
        /// <returns></returns>
        public static bool CheckIfObjectHasPropertyContaingValue(object obj, string propName, string propValue, bool caseSensitive = false)
        {
            var type = obj.GetType();

            var prop = type.GetProperty(propName);
            if (prop != null && prop.PropertyType == typeof(string))
            {
                string value = (caseSensitive) ? prop.GetValue(obj).ToString() : prop.GetValue(obj).ToString().ToLowerInvariant();
                string pattern = (caseSensitive) ? propValue : propValue.ToLowerInvariant();

                if (value.Trim().Contains(pattern.Trim()))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
