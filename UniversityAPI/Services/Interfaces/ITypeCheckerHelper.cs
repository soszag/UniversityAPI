using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAPI.Services.Interfaces
{
    public interface ITypeCheckerHelper
    {
        /// <summary>
        /// Method check if fileds given as aerguments are present in given type.
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        bool CheckIfTypeHasPoperties(string fields, Type type);
    }
}
