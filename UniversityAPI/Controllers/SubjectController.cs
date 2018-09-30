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
    [Route("api/subjects"), Authorize]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private ISubjectRepository subjectRepository;
        private ITypeCheckerHelper typeCheckerHelper;

        public SubjectController(ISubjectRepository subjectRepository, ITypeCheckerHelper typeChecker)
        {
            this.subjectRepository = subjectRepository;
            this.typeCheckerHelper = typeChecker;
        }

        [HttpGet("{id}", Name = "GetSubject")]
        public IActionResult GetSubject([FromRoute] int id)
        {
            var sub = subjectRepository.GetSubjects(new SubjectQueryParameters
            {
                SubjectID = id.ToString()
            });

            if (sub.Count == 0)
            {
                return NotFound("No results found");
            }
            else
            {
                var subDto = Mapper.Map<SubjectDto>(sub[0]);
                return Ok(subDto);
            }
        }

        [HttpGet("GetSubjects", Name = "GetSubjects")]
        public IActionResult GetSubjects([FromQuery] SubjectQueryParameters subjects)
        {
            if (!typeCheckerHelper.CheckIfTypeHasPoperties(subjects.Fields, typeof(SubjectDto)))
            {
                return BadRequest();
            }

            var subjectsFromRepo = subjectRepository.GetSubjects(subjects);

            IEnumerable<SubjectDto> sb = Mapper.Map<IEnumerable<SubjectDto>>(subjectsFromRepo);

            return Ok(sb.ShapeData(subjects.Fields));

        }

        [HttpPost("CreateSubject", Name = "CreateSubject")]
        public IActionResult CreateSubject([FromBody] SubjectCreationDto subject)
        {
            Subjects sb = Mapper.Map<Subjects>(subject);

            subjectRepository.AddSubject(sb, User);

            subjectRepository.Save();
            return CreatedAtRoute("GetClass", new { id = sb.SubjectId }, sb);
        }

        [HttpPut("UpdateSubject", Name = "UpdateSubject")]
        public IActionResult UpdateSubject([FromBody] SubjectUpdateDto updateSubject)
        {
            var sb = Mapper.Map<Subjects>(updateSubject);

            var updateState = subjectRepository.UpdateSubject(sb, User);

            if (updateState == EnumUpdateResult.EntryNotFound)
            {
                return NotFound("Entry to update not found");
            }

            return Ok();
        }

    }
}