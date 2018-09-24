using System.Collections.Generic;
using System.Security.Claims;
using UniversityAPI.Models;

namespace UniversityAPI.Services.HelperObjects
{
    public class LogInResult
    {
        public ICollection<Claim> ClaimsCollection { get; set; }

        public string UserId { get; set; }

        public Users User { get; set; }

        public bool IsValidUser { get; set; }
    }
}
