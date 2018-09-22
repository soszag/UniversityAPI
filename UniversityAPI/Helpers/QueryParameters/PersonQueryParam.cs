using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAPI.Helpers.QueryParameters
{
    public class PersonQueryParam : QueryParametersBase
    {
        // Filtering fields
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string City { get; set; }
        public string Adress { get; set; }
    }
}
