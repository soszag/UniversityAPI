using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAPI.Helpers.QueryParameters
{
    public class UserQueryParameters : QueryParametersBase
    {
        public string OrderBy { get; set; } = "UserName";

        public string UserId { get; set; }
        public bool? IsLogged { get; set; }        
        public string UserName { get; set; }

        public int? TeacherId { get; set; }
        public int? StudentId { get; set; }
        public int? ParentId { get; set; }
       
    }
}
