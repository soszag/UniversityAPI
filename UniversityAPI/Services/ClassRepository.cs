using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UniversityAPI.Helpers;
using UniversityAPI.Helpers.QueryParameters;
using UniversityAPI.Models;
using UniversityAPI.Services.Exceptions;
using UniversityAPI.Services.Interfaces;

namespace UniversityAPI.Services
{
    /// <summary>
    /// Repository for operations over classes set.
    /// </summary>
    public class ClassRepository : BaseRepository, IClassRepository
    {
        public ClassRepository(EduDataContext context)
        {
            this.context = context;
        }

        public void Add(Classes st, ClaimsPrincipal claims)
        {
            context.CreateModificationInformation(st, claims);
            context.Classes.Add(st);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Classes model)
        {
            throw new NotImplementedException();
        }

        public PagedList<Classes> Get(ClassQueryParameters clParam)
        {
            var collection = context.Classes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(clParam.ClassID) && int.TryParse(clParam.ClassID, out var intId))
                collection = collection.Where(cl => cl.ClassId == intId);

            if (!string.IsNullOrWhiteSpace(clParam.Name))
                collection = collection.Where(cl => cl.Name.Contains(clParam.Name, StringComparison.OrdinalIgnoreCase));

            collection = ReflectionHelper.PerformSorting<Classes>(clParam.OrderBy, collection);

            return PagedList<Classes>.Create(collection, clParam.PageNumber, clParam.PageSize);
        }

        public Classes Get(int id)
        {
            return context.Classes.Where(cl => cl.ClassId == id).FirstOrDefault();            
        }

        public override bool Save()
        {
            try
            {
                return base.Save();
            }
            catch (Exception Ex)
            {
                throw new RepositoryException($"Error saving class repository. Error : {Ex.Message}");
            }
        }

        public EnumUpdateResult Update(Classes cl, ClaimsPrincipal claims)
        {
            if (!context.Classes.Contains(cl))
            {
                return EnumUpdateResult.EntryNotFound;
            }
            else
            {
                context.UpdateModificationInformation(cl, claims);
                context.Classes.Update(cl);
                context.Entry(cl).Property(p => p.CreationDate).IsModified = false;
                return EnumUpdateResult.Succesfull;
            }
        }
    }
}
