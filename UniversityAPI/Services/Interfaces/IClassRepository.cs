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
    public interface IClassRepository : IGenericRepository<Classes, ClassQueryParameters>
    {

    }
}
