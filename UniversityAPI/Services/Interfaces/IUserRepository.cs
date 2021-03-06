﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UniversityAPI.Dto;
using UniversityAPI.Dto.CreationDto;
using UniversityAPI.Dto.UpdateDto;
using UniversityAPI.Helpers;
using UniversityAPI.Helpers.QueryParameters;
using UniversityAPI.Models;

namespace UniversityAPI.Services.Interfaces
{
    public enum LoggingUserStatus
    {
        UserExists,
        UserNotExisting,
        UserAlreadyLoggedIn
    }

    public interface IUserRepository : IBaseRepository
    {
        void CreateUser(Users newUser, ClaimsPrincipal claims);

        LoggingUserStatus CheckIfUserExists(LoginDto loginInfo);

        Users GetUserByLoginInformation(LoginDto loginInfo);

        int ChangeUserPassword(UserChangePassword passwordChange);

        PagedList<Users> GetUsers(UserQueryParameters userQuery);

    }
}
