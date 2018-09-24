using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UniversityAPI.Services.Interfaces
{
    public interface IClaimRepository
    {
        ICollection<Claim> GetListOfClaimsForUser(string userId);
    }
}
