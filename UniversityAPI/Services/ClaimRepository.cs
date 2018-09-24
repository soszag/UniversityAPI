using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UniversityAPI.Services.Interfaces;

namespace UniversityAPI.Services
{
    public class ClaimRepository : BaseRepository, IClaimRepository
    {
        public ICollection<Claim> GetListOfClaimsForUser(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
