using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UniversityAPI.Dto;
using UniversityAPI.Dto.CreationDto;
using UniversityAPI.Dto.UpdateDto;
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
            newUser.Password = ComputeSha256Hash(newUser.Password);
            context.Add(newUser);            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        private static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
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

    }
}
