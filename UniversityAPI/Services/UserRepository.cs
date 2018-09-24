using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UniversityAPI.Dto;
using UniversityAPI.Dto.CreationDto;
using UniversityAPI.Dto.UpdateDto;
using UniversityAPI.Helpers;
using UniversityAPI.Models;
using UniversityAPI.Services.Exceptions;
using UniversityAPI.Services.Interfaces;

namespace UniversityAPI.Services
{
    public class UserRepository : BaseRepository, IUserRepository
    {

        public UserRepository(EduDataContext context)
        {
            this.context = context;
        }

        public int ChangeUserPassword(UserChangePassword passwordChange)
        {
            throw new NotImplementedException();
        }

        public LoggingUserStatus CheckIfUserExists(LoginDto loginInfo)
        {
            throw new NotImplementedException();
        }

        public void CreateUser(Users newUser)
        {
            newUser.Password = Utilities.ComputeSha256Hash(newUser.Password);
            context.Add(newUser);            
        }

       
        public override bool Save()
        {
            try
            {
                return base.Save();
            }
            catch (Exception Ex)
            {
                throw new RepositoryException($"Error saving users repository. Error : {Ex.Message}");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public Users GetUserByLoginInformation(LoginDto loginInfo)
        {
            string encodedPassword = Utilities.ComputeSha256Hash(loginInfo.Password);

            var user = context.Users.Where(us => us.Password == encodedPassword && us.UserName == loginInfo.UserName)
                              .FirstOrDefault();

            return user ?? null;            
        }
    }
}
