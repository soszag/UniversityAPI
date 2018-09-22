using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAPI.Helpers;
using UniversityAPI.Helpers.QueryParameters;
using UniversityAPI.Models;
using UniversityAPI.Services.Exceptions;
using UniversityAPI.Services.Interfaces;

namespace UniversityAPI.Services
{
    public class ParentRepository : BaseRepository, IParentRepository
    {
        public ParentRepository(EduDataContext context)
        {
            this.context = context;
        }

        public void AddParent(Parents teacher)
        {
            context.Add(teacher);
        }

        public PagedList<Parents> GetParent(ParentQueryParameters prParam)
        {
            var collection = context.Parents.AsQueryable();

            if (!string.IsNullOrWhiteSpace(prParam.Id) && int.TryParse(prParam.Id, out var intId))
                collection = collection.Where(pr => pr.ParentId == intId);

            if (!string.IsNullOrWhiteSpace(prParam.LastName))
                collection = collection.Where(pr => pr.LastName.Contains(prParam.LastName, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(prParam.FirstName))
                collection = collection.Where(pr => pr.FirstName.Contains(prParam.FirstName, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(prParam.PhoneNumber))
                collection = collection.Where(pr => pr.PhoneNumber.Contains(prParam.PhoneNumber, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(prParam.Email))
                collection = collection.Where(pr => pr.Email.Contains(prParam.Email, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(prParam.City))
                collection = collection.Where(pr => pr.City.Contains(prParam.City, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(prParam.Adress))
                collection = collection.Where(pr => pr.Adress.Contains(prParam.Adress, StringComparison.OrdinalIgnoreCase));

            collection = ReflectionHelper.PerformSorting<Parents>(prParam.OrderBy, collection);

            return PagedList<Parents>.Create(collection, prParam.PageNumber, prParam.PageSize);
        }

        public override bool Save()
        {
            try
            {
                return base.Save();
            }
            catch (Exception Ex)
            {
                throw new RepositoryException($"Error saving parents repository. Error : {Ex.Message}");
            }
        }

        public EnumUpdateResult UpdateParent(Parents teacher)
        {
            throw new NotImplementedException();
        }
    }
}
