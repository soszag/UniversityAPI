using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UniversityAPI.Services.Interfaces
{
    public interface IBaseRepository
    {
        bool Save();
    }
}
