using AutoMapper;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UniversityAPI.Dto;
using UniversityAPI.Helpers;
using UniversityAPI.Services.Interfaces;
using UniversityAPI.Models;
using UniversityAPI.Helpers.QueryParameters;
using UniversityAPI.Dto.CreationDto;
using Microsoft.AspNetCore.Authorization;
using UniversityAPI.Dto.UpdateDto;
using GenericCrudType = UniversityAPI.Services.Interfaces.ICrudGenericService<UniversityAPI.Models.Classes,
                                                                              UniversityAPI.Helpers.QueryParameters.ClassQueryParameters,
                                                                              UniversityAPI.Dto.ClassDto,
                                                                              UniversityAPI.Dto.CreationDto.ClassCreationDto,
                                                                              UniversityAPI.Dto.UpdateDto.ClassUpdateDto>;

namespace UniversityAPI.Controllers
{
    [Route("api/classes"), Authorize]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private IClassRepository classRepository;
        private ITypeCheckerHelper typeCheckerHelper;
        private GenericCrudType crudService;

        public ClassController(IClassRepository classRepository, ITypeCheckerHelper typeChecker, GenericCrudType crudService)
        {
            this.classRepository = classRepository;
            this.typeCheckerHelper = typeChecker;
            this.crudService = crudService;
        }

        [HttpGet("{id}", Name = "GetClass")]
        public IActionResult GetClass([FromRoute] int id)
        {
            return crudService.Get(id);
        }

        [HttpGet("GetClasses", Name = "GetClasses")]
        public IActionResult GetClasses([FromQuery] ClassQueryParameters classes)
        {
            return crudService.Get(classes.Fields, classes);
        }

        [HttpPost("CreateClass", Name = "CreateClass")]
        public IActionResult CreateClass([FromBody] ClassCreationDto classes)
        {
            ClassDto cl = crudService.Create(classes, User);
            return CreatedAtRoute("GetClass", new { id = cl.ID }, cl);
        }

        [HttpPut("UpdateClass", Name ="UpdateClass")]
        public IActionResult UpdateClass([FromBody] ClassUpdateDto updateClass)
        {
            return crudService.Update(updateClass, User);
        }



    }
}