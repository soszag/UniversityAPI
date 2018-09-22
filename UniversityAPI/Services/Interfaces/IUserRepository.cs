using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAPI.Dto;
using UniversityAPI.Dto.CreationDto;
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
        void CreateUser(Users newUser);

        LoggingUserStatus CheckIfUserExists(LoginDto loginInfo);

        int ChangeUserPassword(UserChangePassword passwordChange);
    }
}
