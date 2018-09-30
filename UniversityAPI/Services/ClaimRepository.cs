using System.Collections.Generic;
using System.Linq;
using UniversityAPI.Models;
using UniversityAPI.Services.Interfaces;

namespace UniversityAPI.Services
{
    public class ClaimRepository : BaseRepository, IClaimRepository
    {
        public ClaimRepository(EduDataContext context)
        {
            this.context = context;
        }

        public void AddClaims(IEnumerable<Claims> claims)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<Claims> GetListOfClaimsForUser(string userId)
        {
            List<Claims> retList = new List<Claims>();

            if (int.TryParse(userId, out int uId))
            {
                retList =   (from c in context.Claims
                             join uc in context.UsersClaims
                             on c.ClaimId equals uc.ClaimId
                             where uc.UserId == uId
                             select c).ToList();
            }

            return retList;
        }
    }
}
