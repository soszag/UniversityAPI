using System;
using System.Collections.Generic;

namespace UniversityAPI.Models
{
    public partial class UsersClaims
    {
        public int UserId { get; set; }
        public int ClaimId { get; set; }

        public Claims Claim { get; set; }
        public Users User { get; set; }
    }
}
