using System;
using System.Collections.Generic;

namespace UniversityAPI.Models
{
    public partial class Parents
    {
        public Parents()
        {
            ParentStudent = new HashSet<ParentStudent>();
            Users = new HashSet<Users>();
        }

        public int ParentId { get; set; }
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
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public int? ModifiedBy { get; set; }

        public ICollection<ParentStudent> ParentStudent { get; set; }
        public ICollection<Users> Users { get; set; }
    }
}
