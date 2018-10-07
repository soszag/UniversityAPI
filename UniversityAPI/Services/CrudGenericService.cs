using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UniversityAPI.Helpers;
using UniversityAPI.Services.Interfaces;

namespace UniversityAPI.Services
{
    public class CrudGenericService<TModel, TQueryParameters, TDto, TCreationDto, TUpdateDto> :
                 ICrudGenericService<TModel, TQueryParameters, TDto, TCreationDto, TUpdateDto>
    {

        private IGenericRepository<TModel, TQueryParameters> repository;
        private ITypeCheckerHelper typeCheckerHelper;

        public CrudGenericService(IGenericRepository<TModel, TQueryParameters> repo, ITypeCheckerHelper typeChecker)
        {
            repository = repo;
            typeCheckerHelper = typeChecker;
        }

        public TDto Create(TCreationDto creationDto, ClaimsPrincipal claims)
        {
            TModel toCreate = Mapper.Map<TModel>(creationDto);

            repository.Add(toCreate, claims);
            repository.Save();

            TDto createdDto = Mapper.Map<TDto>(toCreate);

            return createdDto;            
        }

        public IActionResult Delete(TModel model, ClaimsPrincipal claims)
        {
            throw new NotImplementedException();
        }

        public IActionResult Delete(int id, ClaimsPrincipal claims)
        {
            throw new NotImplementedException();
        }

        public IActionResult Get(string fields, TQueryParameters queryParams)
        {
            if (typeCheckerHelper.CheckIfTypeHasPoperties(fields, typeof(TDto)))
            {
                return new BadRequestResult();
            }

            var fromRepo = repository.Get(queryParams);
            IEnumerable<TDto> mapped = Mapper.Map<IEnumerable<TDto>>(fromRepo);

            return new OkObjectResult(mapped.ShapeData(fields));
        }

        public IActionResult Get(int id)
        {
            TModel fromRepo = repository.Get(id);

            if (fromRepo == null)
            {
                return new NotFoundObjectResult("No results found");
            }
            else
            {
                TDto mapped = Mapper.Map<TDto>(fromRepo);
                return new OkObjectResult(mapped);
            }            
        }

        public IActionResult Update(TUpdateDto updateDto, ClaimsPrincipal claims)
        {
            TModel toUpdate = Mapper.Map<TModel>(updateDto);

            var updateState = repository.Update(toUpdate, claims);

            if (updateState == EnumUpdateResult.EntryNotFound)
            {
                return new NotFoundObjectResult("Entry to update not found");
            }
            else
            {
                return new OkResult();
            }
        }
    }
}
