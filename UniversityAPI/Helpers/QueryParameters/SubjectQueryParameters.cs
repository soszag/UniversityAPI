using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAPI.Helpers.QueryParameters
{
    public class SubjectQueryParameters : QueryParametersBase
    {
        public string OrderBy { get; set; } = "Name";

        public string SubjectID { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public string Description { get; set; }
    }
}
