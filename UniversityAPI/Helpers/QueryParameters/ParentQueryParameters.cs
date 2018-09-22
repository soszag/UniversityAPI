using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAPI.Helpers.QueryParameters
{
    public class ParentQueryParameters : PersonQueryParam
    {
        public string OrderBy { get; set; } = "LastName";
        /// <summary>
        /// Id's of childrens (comma separated) for given parent
        /// </summary>
        public string ChildrensIds { get; set; }

    }
}
