using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniversityAPI.Dto;
using UniversityAPI.Helpers;
using UniversityAPI.Services.Interfaces;
using UniversityAPI.Models;
using UniversityAPI.Helpers.QueryParameters;
using UniversityAPI.Dto.CreationDto;
using Microsoft.AspNetCore.Authorization;
using UniversityAPI.Dto.UpdateDto;

namespace UniversityAPI.Controllers
{
    [Route("api/classes"), Authorize]
    public class ClassController : Controller
    {
        private IClassRepository classRepository;
        private ITypeCheckerHelper typeCheckerHelper;

        public ClassController(IClassRepository classRepository, ITypeCheckerHelper typeChecker)
        {
            this.classRepository = classRepository;
            this.typeCheckerHelper = typeChecker;
        }

        [HttpGet("{id}", Name = "GetClass")]
        public IActionResult GetClass([FromRoute] int id)
        {
            var cl = classRepository.GetClasses( new ClassQueryParameters 
            {
                ClassID = id.ToString()
            });

            if (cl.Count == 0)
            {
                return NotFound("No results found");
            }
            else
            {
                var stDto = Mapper.Map<ClassDto>(cl[0]);
                return Ok(stDto);
            }
        }

        [HttpGet("GetClasses", Name = "GetClasses")]
        public IActionResult GetClasses([FromQuery] ClassQueryParameters classes)
        {
            if(!typeCheckerHelper.CheckIfTypeHasPoperties(classes.Fields, typeof(ClassDto)))
            {
                return BadRequest();
            }

            var classesFromRepo = classRepository.GetClasses(classes);

            IEnumerable<ClassDto> cl = Mapper.Map<IEnumerable<ClassDto>>(classesFromRepo);

            return Ok(cl.ShapeData(classes.Fields));

        }

        [HttpPost("CreateClass", Name = "CreateClass")]
        public IActionResult CreateClass([FromBody] ClassCreationDto classes)
        {
            Classes cl = Mapper.Map<Classes>(classes);

            classRepository.AddClass(cl);

            classRepository.Save();
            return CreatedAtRoute("GetClass", new { id = cl.ClassId }, cl);
        }

        [HttpPut("UpdateClass", Name ="UpdateClass")]
        public IActionResult UpdateClass([FromBody] ClassUpdateDto updateClass)
        {
            var cl = Mapper.Map<Classes>(updateClass);

            var updateState = classRepository.UpdateClass(cl);

            if (updateState == EnumUpdateResult.EntryNotFound)
            {
                return NotFound("Entry to update not found");
            }

            return Ok();
        }



    }
}