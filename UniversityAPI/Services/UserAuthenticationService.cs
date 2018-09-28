using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UniversityAPI.Dto;
using UniversityAPI.Dto.CreationDto;
using UniversityAPI.Helpers.QueryParameters;
using UniversityAPI.Models;
using UniversityAPI.Services.HelperObjects;
using UniversityAPI.Services.Interfaces;

namespace UniversityAPI.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        public const string CLAIM_USER_ID = "UserId";

        private IUserRepository userRepo;
        private IClaimRepository claimRepo;
        private IStudentRepository studentRepo;
        private ITeacherRepository teacherRepo;
        private IParentRepository parentRepo;

        public UserAuthenticationService(IUserRepository userRepository, IClaimRepository claimRepository,
                                         IStudentRepository stRepo, ITeacherRepository trRepo, IParentRepository prRepo)
        {
            this.userRepo = userRepository;
            this.claimRepo = claimRepository;
            this.teacherRepo = trRepo;
            this.studentRepo = stRepo;
            this.parentRepo = prRepo;
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
            LogInResult result = new LogInResult()
            {
                IsValidUser = false
            };

            Users user = userRepo.GetUserByLoginInformation(login);
            
            if (user != null)
            {
                // Retrieve claims of current user
                result.ClaimsCollection = Mapper.Map<ICollection<Claim>>(claimRepo.GetListOfClaimsForUser(user.UserId.ToString()));

                // Additionaly add userId as a claim
                result.ClaimsCollection.Add(new Claim(CLAIM_USER_ID, user.UserId.ToString(), ""));

                result.UserId = user.UserId.ToString();
                result.User = user;
                result.IsValidUser = true;
            }

            return result;
        }

        public CreateUserResult CreateNewUser(UserCreationDto createUser)
        {
            CreateUserResult result = new CreateUserResult()
            {
                CreationError = UserCreationError.NoError,
                IsCreatedSuccesfully = true
            };

            // First check if user alredy exists in database
            var users = userRepo.GetUsers(new UserQueryParameters()
            {
                UserName = createUser.UserName
            });

            if (users.Any(u => string.Compare(u.UserName, createUser.UserName, StringComparison.OrdinalIgnoreCase) == 0))
            {
                result.IsCreatedSuccesfully = false;
                result.CreationError = UserCreationError.LoginAlreadyExists;
                return result;
            }

            Users user = Mapper.Map<Users>(createUser);
            
            // Create appropriate user data according to flags
            if (createUser.IsTeacher)
            {
                Teachers teacher = Mapper.Map<Teachers>(createUser);
                teacherRepo.AddTeacher(teacher);
                teacherRepo.Save();
                user.TeacherId = teacher.TeacherId;
                result.CreatedUser = Mapper.Map<UserDto>(teacher);
            }

            if (createUser.IsStudent)
            {
                Students student = Mapper.Map<Students>(createUser);
                studentRepo.AddStudent(student);
                studentRepo.Save();
                user.StudentId = student.StudentId;
                result.CreatedUser = Mapper.Map<UserDto>(student);
            }

            if (createUser.IsParent)
            {
                Parents parent = Mapper.Map<Parents>(createUser);
                parentRepo.AddParent(parent);
                parentRepo.Save();
                user.ParentId = parent.ParentId;
                result.CreatedUser = Mapper.Map<UserDto>(parent);
            }

            userRepo.CreateUser(user);
            userRepo.Save();
            result.CreatedUser.UserId = user.UserId.ToString();

            return result;
        }

    }
}
