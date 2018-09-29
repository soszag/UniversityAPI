using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityAPI.Dto;
using UniversityAPI.Dto.CreationDto;
using UniversityAPI.Helpers.QueryParameters;
using UniversityAPI.Models;
using UniversityAPI.Services.Exceptions;
using UniversityAPI.Services.HelperObjects;
using UniversityAPI.Services.Interfaces;

namespace UniversityAPI.Controllers
{
    [Route("api/auth"), Authorize]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserRepository userRepo;
        private IUserAuthenticationService authService;

        public AuthController(IUserRepository userRepo, IUserAuthenticationService authService)
        {
            this.userRepo = userRepo;
            this.authService = authService;
        }

        [HttpPost("Login", Name = "Login"), AllowAnonymous]
        public IActionResult Login([FromBody] LoginDto user)
        {
            var loginResult = authService.PerformLogInAction(user);

            if(loginResult.IsValidUser)
            {
                string token = authService.GenerateToken(user, loginResult.ClaimsCollection);
                return Ok(new { Token = token });
            }
            else
            {
                return BadRequest("Validation failure. Unrecognized user.");
            }
            
        }

        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult GetUser ([FromRoute] int id)
        {
            var user = userRepo.GetUsers(new UserQueryParameters { UserId = id.ToString() }).FirstOrDefault();

            if (user == null)
            {
                return NotFound("User not found");
            }
            else
            {
                UserDto u = Mapper.Map<UserDto>(user);
                return Ok(u);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        [HttpPost("CreateUser", Name = "CreateUser")]
        public IActionResult CreateUser([FromBody] UserCreationDto newUser)
        {
            var user = authService.CreateNewUser(newUser, User);

            if (!user.IsCreatedSuccesfully)
            {
                if(user.CreationError == UserCreationError.LoginAlreadyExists)
                {
                    return BadRequest("Cannot create user. User with given user name already exists");
                }
                else
                {
                    return BadRequest("Cannot create user. Unknown error");
                }
            }
            else
            {
                return CreatedAtRoute("GetUser", new { id = user.CreatedUser.UserId }, user.CreatedUser);
            }
            

        }

    }
}