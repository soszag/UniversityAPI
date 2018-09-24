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
    public class TeacherRepository : BaseRepository, ITeacherRepository
    {
        public TeacherRepository(EduDataContext context)
        {
            this.context = context;
        }

        public void AddTeacher(Teachers teacher)
        {
            context.Add(teacher);
        }

        public PagedList<Teachers> GetTeachers(TeacherQueryParameters tParam)
        {
            var collection = context.Teachers.AsQueryable();
            if (!string.IsNullOrWhiteSpace(tParam.Id) && int.TryParse(tParam.Id, out var intId))
                collection = collection.Where(st => st.TeacherId == intId);

            if (!string.IsNullOrWhiteSpace(tParam.LastName))
                collection = collection.Where(st => st.LastName.Contains(tParam.LastName, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(tParam.FirstName))
                collection = collection.Where(st => st.FirstName.Contains(tParam.FirstName, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(tParam.PhoneNumber))
                collection = collection.Where(st => st.PhoneNumber.Contains(tParam.PhoneNumber, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(tParam.Email))
                collection = collection.Where(st => st.Email.Contains(tParam.Email, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(tParam.City))
                collection = collection.Where(st => st.City.Contains(tParam.City, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(tParam.Adress))
                collection = collection.Where(st => st.Adress.Contains(tParam.Adress, StringComparison.OrdinalIgnoreCase));

            collection = ReflectionHelper.PerformSorting<Teachers>(tParam.OrderBy, collection);

            return PagedList<Teachers>.Create(collection, tParam.PageNumber, tParam.PageSize);
        }

        public override bool Save()
        {
            try
            {
                return base.Save();
            }
            catch (Exception Ex)
            {
                throw new RepositoryException($"Error saving teachers repository. Error : {Ex.Message}");
            }
        }

        public EnumUpdateResult UpdateTeacher(Teachers teacher)
        {
            if (!context.Teachers.Contains(teacher))
            {
                return EnumUpdateResult.EntryNotFound;
            }
            else
            {
                context.Teachers.Update(teacher);
                return EnumUpdateResult.Succesfull;
            }
        }
    }
}
