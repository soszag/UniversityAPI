using System.Collections.Generic;
using UniversityAPI.Models;

namespace UniversityAPI.Services.Interfaces
{
    public interface IClaimRepository
    {
        ICollection<Claims> GetListOfClaimsForUser(string userId);
    }
}
