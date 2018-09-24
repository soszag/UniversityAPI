using System.Collections.Generic;
using UniversityAPI.Models;
using UniversityAPI.Services.Interfaces;

namespace UniversityAPI.Services
{
    public class ClaimRepository : BaseRepository, IClaimRepository
    {
        public ICollection<Claims> GetListOfClaimsForUser(string userId)
        {
            return new List<Claims>() { };
        }
    }
}
