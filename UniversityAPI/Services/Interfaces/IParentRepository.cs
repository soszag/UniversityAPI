using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAPI.Helpers;
using UniversityAPI.Helpers.QueryParameters;
using UniversityAPI.Models;

namespace UniversityAPI.Services.Interfaces
{
    public interface IParentRepository : IBaseRepository
    {
        PagedList<Parents> GetParent(ParentQueryParameters parentResourceParameters);

        void AddParent(Parents parent);

        EnumUpdateResult UpdateParent(Parents parent);
    }
}
