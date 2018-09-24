using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAPI.Dto;

namespace UniversityAPI.Services.Interfaces
{
    [Obsolete("WARNING !! Remove this when new service is done.")]
    public interface IJWTService
    {
        string GenerateToken(LoginDto user);
    }
}
