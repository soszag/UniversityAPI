using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UniversityAPI.Dto;
using UniversityAPI.Dto.CreationDto;
using UniversityAPI.Models;
using UniversityAPI.Services.HelperObjects;

namespace UniversityAPI.Services.Interfaces
{
    public interface IUserAuthenticationService
    {
        LogInResult PerformLogInAction(LoginDto login);

        string GenerateToken(LoginDto user, IEnumerable<Claim> claims);

        CreateUserResult CreateNewUser(UserCreationDto createUser);

    }
}
