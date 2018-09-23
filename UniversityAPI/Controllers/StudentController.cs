using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using UniversityAPI.Dto;
using UniversityAPI.Dto.CreationDto;
using UniversityAPI.Dto.UpdateDto;
using UniversityAPI.Helpers;
using UniversityAPI.Helpers.QueryParameters;
using UniversityAPI.Models;
using UniversityAPI.Services.Interfaces;

namespace UniversityAPI.Controllers
{
    [Route("api/students"), Authorize]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private IStudentRepository studentsRepo;
        private ITypeCheckerHelper typeChecker;

        public StudentController(IStudentRepository studentsRepository, ITypeCheckerHelper typeCheckerHelper)
        {
            studentsRepo = studentsRepository;
            typeChecker = typeCheckerHelper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetStudent")]
        public IActionResult GetStudent([FromRoute] int id)
        {
            var st = studentsRepo.GetStudents(new StudentQueryParameters
            {
                Id = id.ToString()
            });
            
            if (st.Count == 0)
            {
                return NotFound("No results found");
            }
            else
            {
                var stDto = Mapper.Map<StudentDto>(st[0]);
                return Ok(stDto);
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentResourceParameters"></param>
        /// <returns></returns>
        [HttpGet("GetStudents", Name = "GetStudents")]
        public IActionResult GetStudents([FromQuery] StudentQueryParameters studentResourceParameters)
        {
            if (!typeChecker.CheckIfTypeHasPoperties(studentResourceParameters.Fields, typeof(StudentDto)))
            {
                return BadRequest();
            }

            // Get data from repository (including sorting and filtering)
            var studentsFromRepo = studentsRepo.GetStudents(studentResourceParameters);

            // Map from Students enitty to StudentsDto
            IEnumerable<StudentDto> students = Mapper.Map<IEnumerable<StudentDto>>(studentsFromRepo);
            
            // Perform data shaping and return value            
            return Ok(students.ShapeData(studentResourceParameters.Fields));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost("CreateStudent", Name = "CreateStudent")]
        public IActionResult CreateStudent([FromBody] StudentCreationDto student)
        {
            Students st = Mapper.Map<Students>(student);

            studentsRepo.AddStudent(st);

            studentsRepo.Save();

            return CreatedAtRoute("GetStudent", new { st.StudentId }, st);
        }

        [HttpPut("UpdateStudent", Name = "UpdateStudent")]
        public IActionResult UpdateStudent([FromBody] StudentUpdateDto st)
        {
            var student = Mapper.Map<Students>(st);

            var updateState = studentsRepo.UpdateStudent(student);

            if (updateState == EnumUpdateResult.EntryNotFound)
            {
                return NotFound("Entry to update not found");
            }

            return Ok();
        }
        
    }
}
