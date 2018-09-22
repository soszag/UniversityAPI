using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAPI.Dto
{
    public class PersonDto : BaseTypeDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Adress { get; set; }
        public string PostalCode { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
}
