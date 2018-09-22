using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAPI.Helpers.QueryParameters
{
    /// <summary>
    /// Base class for all query parameters classes.
    /// </summary>
    public class QueryParametersBase
    {
        /// <summary>
        /// 
        /// </summary>
        const int maxPageSize = 40;

        public int PageNumber { get; set; } = 1;

        private int pageSize = 20;
        
        public int PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {
                pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }


        public string SearchQuery { get; set; }

        /// <summary>
        /// Comma separated collection of fields for objects to include in repsonse. When leaved empty all fields will be returned
        /// </summary>
        public string Fields { get; set; }
    }
}
