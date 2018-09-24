using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityAPI.Dto;
using UniversityAPI.Dto.CreationDto;
using UniversityAPI.Dto.UpdateDto;
using UniversityAPI.Helpers;
using UniversityAPI.Helpers.QueryParameters;
using UniversityAPI.Models;
using UniversityAPI.Services.Interfaces;

namespace UniversityAPI.Controllers
{
    [Route("api/teachers"), Authorize]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private ITeacherRepository teacherRepo;
        private ITypeCheckerHelper typeCheckerHelper;

        public TeacherController(ITeacherRepository repo, ITypeCheckerHelper typeChecker)
        {
            this.teacherRepo = repo;
            this.typeCheckerHelper = typeChecker;
        }

        [HttpGet("{id}", Name = "GetTeacher")]
        public IActionResult GetTeacher([FromRoute] int id)
        {
            var t = teacherRepo.GetTeachers(new TeacherQueryParameters
            {
                Id = id.ToString()
            });
            
            if (t.Count == 0)
            {
                return NotFound("No results found");
            }
            else
            {
                var stDto = Mapper.Map<TeacherDto>(t[0]);
                return Ok(stDto);
            }
        }

        [HttpGet("GetTeachers", Name = "GetTeachers")]
        public IActionResult GetParent([FromQuery] TeacherQueryParameters teacherQuery)
        {
            if (!typeCheckerHelper.CheckIfTypeHasPoperties(teacherQuery.Fields, typeof(TeacherDto)))
            {
                return BadRequest();
            }
            var userId = User.Claims.Where(c => c.Type == "UserId").FirstOrDefault()?.Value;

            var teachersFromRepo = teacherRepo.GetTeachers(teacherQuery);

            IEnumerable<TeacherDto> t = Mapper.Map<IEnumerable<TeacherDto>>(teachersFromRepo);

            return Ok(t.ShapeData(teacherQuery.Fields));
        }

        [HttpPost("CreateTeacher", Name = "CreateTeacher")]
        public IActionResult CreateClass([FromBody] TeacherCreationDto teacher)
        {
            Teachers t = Mapper.Map<Teachers>(teacher);

            teacherRepo.AddTeacher(t);

            teacherRepo.Save();

            return CreatedAtRoute("GetTeacher", new { id = t.TeacherId }, t);
        }

        [HttpPut("UpdateTeacher", Name = "UpdateTeacher")]
        public IActionResult UpdateClass([FromBody] TeacherUpdateDto updateTeacher)
        {
            var t = Mapper.Map<Teachers>(updateTeacher);

            var updateState = teacherRepo.UpdateTeacher(t);

            if (updateState == EnumUpdateResult.EntryNotFound)
            {
                return NotFound("Entry to update not found");
            }

            teacherRepo.Save();

            return Ok();
        }


    }
}