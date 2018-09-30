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
    public class SubjectRepository : BaseRepository, ISubjectRepository
    {
        public SubjectRepository(EduDataContext context)
        {
            this.context = context;
        }

        public void AddSubject(Subjects subject, ClaimsPrincipal claims)
        {
            context.CreateModificationInformation(subject, claims);
            context.Add(subject);
        }

        public PagedList<Subjects> GetSubjects(SubjectQueryParameters sParam)
        {
            var collection = context.Subjects.AsQueryable();
            if (!string.IsNullOrWhiteSpace(sParam.SubjectID) && int.TryParse(sParam.SubjectID, out var intId))
                collection = collection.Where(sb => sb.SubjectId == intId);

            if (!string.IsNullOrWhiteSpace(sParam.Name))
                collection = collection.Where(sb => sb.Name.Contains(sParam.Name, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(sParam.ShortName))
                collection = collection.Where(sb => sb.ShortName.Contains(sParam.ShortName, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(sParam.Description))
                collection = collection.Where(sb => sb.Description.Contains(sParam.Description, StringComparison.OrdinalIgnoreCase));

            collection = ReflectionHelper.PerformSorting<Subjects>(sParam.OrderBy, collection);

            return PagedList<Subjects>.Create(collection, sParam.PageNumber, sParam.PageSize);
        }

        public EnumUpdateResult UpdateSubject(Subjects subject, ClaimsPrincipal claims)
        {
            if (!context.Subjects.Contains(subject))
            {
                return EnumUpdateResult.EntryNotFound;
            }
            else
            {
                context.UpdateModificationInformation(subject, claims);
                context.Subjects.Update(subject);
                context.Entry(subject).Property(p => p.CreationDate).IsModified = false;
                return EnumUpdateResult.Succesfull;
            }
        }

        public override bool Save()
        {
            try
            {
                return base.Save();
            }
            catch (Exception Ex)
            {
                throw new RepositoryException($"Error saving subjects repository. Error : {Ex.Message}");
            }
        }
    }
}
