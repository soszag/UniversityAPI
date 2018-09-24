using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UniversityAPI.Models;

namespace UniversityAPI.Services.HelperObjects
{
    public class LogInResult
    {
        public IEnumerable<Claim> ClaimsCollection { get; set; }

        public string UserId { get; set; }

        public Users User { get; set; }
    }
}
