using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAPI.Dto.CreationDto
{
    public class BasePersonCreationDto
    {
        [MinLength(2, ErrorMessage = "Field 'FirstName' must have minimum 2 characters")]
        public string FirstName { get; set; }

        [MinLength(2, ErrorMessage = "Field 'LastName' must have minimum 2 characters")]
        public string LastName { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public string Adress { get; set; }
        public string PostalCode { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}
