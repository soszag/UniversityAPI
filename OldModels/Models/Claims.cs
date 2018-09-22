using System;
using System.Collections.Generic;

namespace UniversityAPI.Models
{
    public partial class Claims
    {
        public Claims()
        {
            UsersClaims = new HashSet<UsersClaims>();
        }

        public int ClaimId { get; set; }
        public string ClaimName { get; set; }
        public string ClaimParameters { get; set; }

        public ICollection<UsersClaims> UsersClaims { get; set; }
    }
}
