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
    public interface IClassRepository : IBaseRepository
    {
        PagedList<Classes> GetClasses(ClassQueryParameters classResourceParameters);

        void AddClass(Classes st, ClaimsPrincipal claims);

        EnumUpdateResult UpdateClass(Classes cl, ClaimsPrincipal claims);
    }
}
