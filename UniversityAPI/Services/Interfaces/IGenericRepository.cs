using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UniversityAPI.Helpers;

namespace UniversityAPI.Services.Interfaces
{
    public interface IGenericRepository<TModel, TQueryParameters>
    {
        void Add(TModel obj, ClaimsPrincipal claims);

        PagedList<TModel> Get(TQueryParameters queryParams);

        TModel Get(int id);

        EnumUpdateResult Update(TModel model, ClaimsPrincipal claims);

        void Delete(int id);

        void Delete(TModel model);

        bool Save();

    }
}
