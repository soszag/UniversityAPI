using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UniversityAPI.Services.Interfaces;

namespace UniversityAPI.Services
{
    public class TypeCheckerHelper : ITypeCheckerHelper
    {
        /// <summary>
        /// Method check if fileds given as aerguments are present in given type.
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool CheckIfTypeHasPoperties(string fields, Type type)
        {
            if (fields == null)
            {
                return true;
            }

            var splitted = fields.Split(',');

            foreach (var field in splitted)
            {
                var propertyInfo = type.GetProperty(field.Trim(), BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);

                if (propertyInfo == null)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
