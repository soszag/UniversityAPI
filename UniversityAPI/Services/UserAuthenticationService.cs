using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UniversityAPI.Dto;
using UniversityAPI.Models;
using UniversityAPI.Services.HelperObjects;
using UniversityAPI.Services.Interfaces;

namespace UniversityAPI.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        IUserRepository userRepo;
        IClaimRepository claimRepo;

        public UserAuthenticationService(IUserRepository userRepository, IClaimRepository claimRepository)
        {
            this.userRepo = userRepository;
            this.claimRepo = claimRepository;
        }

        public string GenerateToken(LoginDto user, IEnumerable<Claim> claims)
        {
            string tokenToReturn = null;
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("wiElKi_595_CLM_$!_52()=?"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:57324/",
                audience: "http://localhost:57324/",
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signinCredentials
            );

            tokenToReturn = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return tokenToReturn;
        }

        public LogInResult PerformLogInAction(LoginDto login)
        {
            LogInResult result = new LogInResult();
            Users user = userRepo.GetUserByLoginInformation(login);
            
            if (user != null)
            {
                // Retrieve claims of current user
                result.ClaimsCollection = claimRepo.GetListOfClaimsForUser(user.UserId.ToString());
                result.UserId = user.UserId.ToString();
                result.User = user;
            }

            return result;
        }
    }
}
