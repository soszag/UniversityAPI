using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UniversityAPI.Services.Interfaces
{
    public interface ICrudGenericService<TModel, TQueryParameters, TDto, TCreationDto, TUpdateDto>
    {
        IActionResult Get(string fields, TQueryParameters queryParams);

        IActionResult Get(int id);

        TDto Create(TCreationDto creationDto, ClaimsPrincipal claims);

        IActionResult Update(TUpdateDto updateDto, ClaimsPrincipal claims);

        IActionResult Delete(TModel model, ClaimsPrincipal claims);

        IActionResult Delete(int id, ClaimsPrincipal claims);
    }
}
