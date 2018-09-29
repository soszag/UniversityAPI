using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UniversityAPI.Helpers;
using UniversityAPI.Helpers.QueryParameters;
using UniversityAPI.Models;

namespace UniversityAPI.Services.Interfaces
{
    public interface IParentRepository : IBaseRepository
    {
        PagedList<Parents> GetParent(ParentQueryParameters parentResourceParameters);

        void AddParent(Parents parent, ClaimsPrincipal claims);

        EnumUpdateResult UpdateParent(Parents parent, ClaimsPrincipal claims);
    }
}
