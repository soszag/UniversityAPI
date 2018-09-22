using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAPI.Dto;

namespace UniversityAPI.Services.Interfaces
{
    public interface IJWTService
    {
        string GenerateToken(LoginDto user);
    }
}
