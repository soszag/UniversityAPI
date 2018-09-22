using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAPI.Helpers.QueryParameters
{
    public class TeacherQueryParameters : PersonQueryParam
    {
        public string OrderBy { get; set; } = "LastName";
    }
}
