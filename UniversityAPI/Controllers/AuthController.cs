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
using UniversityAPI.Models;
using UniversityAPI.Services.Exceptions;
using UniversityAPI.Services.Interfaces;

namespace UniversityAPI.Controllers
{
    [Route("api/auth"), Authorize]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IJWTService jwtService;
        private IUserRepository userRepo;
        private IStudentRepository studentRepo;

        public AuthController(IJWTService jwtService, IUserRepository userRepo, IStudentRepository studentRepo)
        {
            this.jwtService = jwtService;
            this.userRepo = userRepo;
            this.studentRepo = studentRepo;
        }

        [HttpPost("Login", Name = "Login"), AllowAnonymous]
        public IActionResult Login([FromBody] LoginDto user)
        {            
            var token = jwtService.GenerateToken(user);

            if (token != null)
                return Ok(new { Token = token });
            else
                return BadRequest();

        }

        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult GetUser ([FromQuery] int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        [HttpPost("CreateUser", Name = "CreateUser")]
        public IActionResult CreateUser([FromBody] UserCreationDto newUser)
        {
            Users user = Mapper.Map<Users>(newUser);

            // Create appropriate user data according to flags
            if (newUser.IsTeacher)
            {
                Teachers teacher = Mapper.Map<Teachers>(newUser);
                // ...
            }
            
            if (newUser.IsStudent)
            {
                Students student = Mapper.Map<Students>(newUser);
                studentRepo.AddStudent(student);
                studentRepo.Save();
                user.StudentId = student.StudentId;
            }

            if (newUser.IsParent)
            {
                Parents parent = Mapper.Map<Parents>(newUser);
                //...
            }

            userRepo.CreateUser(user);
            userRepo.Save();

            UserCreationDto withoutPassword = newUser;
            withoutPassword.Password = "";

            return CreatedAtRoute("GetUser", new { id = user.UserId }, withoutPassword);

        }

    }
}