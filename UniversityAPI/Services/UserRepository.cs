using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UniversityAPI.Dto;
using UniversityAPI.Dto.CreationDto;
using UniversityAPI.Dto.UpdateDto;
using UniversityAPI.Helpers;
using UniversityAPI.Helpers.QueryParameters;
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

        public void CreateUser(Users newUser, ClaimsPrincipal claims)
        {
            context.CreateModificationInformation(newUser, claims);
            newUser.Password = Utilities.ComputeSha256Hash(newUser.Password);
            context.Add(newUser);            
        }

        public PagedList<Users> GetUsers(UserQueryParameters userQuery)
        {
            var collection = context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(userQuery.UserId) && int.TryParse(userQuery.UserId, out var intId))
                collection = collection.Where(u => u.UserId == intId);
            if (!string.IsNullOrWhiteSpace(userQuery.UserName))
                collection = collection.Where(u => u.UserName.Contains(userQuery.UserName));
            if (userQuery.IsLogged != null)
                collection = collection.Where(u => u.IsLogged == true);

            if (userQuery.TeacherId != null)
                collection = collection.Where(u => u.TeacherId == userQuery.TeacherId);
            if (userQuery.StudentId != null)
                collection = collection.Where(u => u.StudentId == userQuery.StudentId);
            if (userQuery.ParentId != null)
                collection = collection.Where(u => u.ParentId == userQuery.ParentId);

            collection = ReflectionHelper.PerformSorting<Users>(userQuery.OrderBy, collection);

            return PagedList<Users>.Create(collection, userQuery.PageNumber, userQuery.PageSize);
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
