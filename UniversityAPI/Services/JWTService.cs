using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UniversityAPI.Dto;

namespace UniversityAPI.Services.Interfaces
{
    [Obsolete("WARNING !! Remove this when new service is done.")]
    public class JWTService : IJWTService
    {
        public string GenerateToken(LoginDto user)
        {
            string tokenToReturn = null;

            if (user != null)
            {
                if (user.UserName == "socha" && user.Password == "x")
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("wiElKi_595_CLM_$!_52()=?"));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var tokeOptions = new JwtSecurityToken(
                        issuer: "http://localhost:57324/",
                        audience: "http://localhost:57324/",
                        claims: new List<Claim>() { new Claim("UserId", "150", "ValueType") },
                        expires: DateTime.Now.AddMinutes(60),
                        signingCredentials: signinCredentials
                    );

                    tokenToReturn = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                }
            }

            return tokenToReturn;
        }
    }
}
