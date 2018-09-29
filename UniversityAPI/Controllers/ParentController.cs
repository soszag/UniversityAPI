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
    [Route("api/parents"), Authorize]
    [ApiController]
    public class ParentController : ControllerBase
    {
        private IParentRepository parentRepo;
        private ITypeCheckerHelper typeCheckerHelper;

        public ParentController(IParentRepository repo, ITypeCheckerHelper typeChecker)
        {
            this.parentRepo = repo;
            this.typeCheckerHelper = typeChecker;
        }

        [HttpGet("{id}", Name = "GetParent")]
        public IActionResult GetParent([FromRoute] int id)
        {
            var cl = parentRepo.GetParent(new ParentQueryParameters
            {
                Id = id.ToString()
            });

            if (cl.Count == 0)
            {
                return NotFound("No results found");
            }
            else
            {
                var stDto = Mapper.Map<ParentDto>(cl[0]);
                return Ok(stDto);
            }
        }

        [HttpGet("GetParents", Name = "GetParents")]
        public IActionResult GetParents([FromQuery] ParentQueryParameters parentQuery)
        {
            if (!typeCheckerHelper.CheckIfTypeHasPoperties(parentQuery.Fields, typeof(ParentDto)))
            {
                return BadRequest();
            }

            var parentsFromRepo = parentRepo.GetParent(parentQuery);

            IEnumerable<ParentDto> cl = Mapper.Map<IEnumerable<ParentDto>>(parentsFromRepo);

            return Ok(cl.ShapeData(parentQuery.Fields));
        }

        [HttpPost("CreateParent", Name = "CreateParent")]
        public IActionResult CreateClass([FromBody] ParentCreationDto parent)
        {
            Parents pr = Mapper.Map<Parents>(parent);

            parentRepo.AddParent(pr, User);

            parentRepo.Save();
            return CreatedAtRoute("GetParent", new { id = pr.ParentId }, pr);
        }

        [HttpPut("UpdateParent", Name = "UpdateParent")]
        public IActionResult UpdateClass([FromBody] ParentUpdateDto updateParent)
        {
            var pr = Mapper.Map<Parents>(updateParent);

            var updateState = parentRepo.UpdateParent(pr, User);

            if (updateState == EnumUpdateResult.EntryNotFound)
            {
                return NotFound("Entry to update not found");
            }

            return Ok();
        }
    }
}