using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UniversityAPI.Helpers;
using UniversityAPI.Helpers.QueryParameters;
using UniversityAPI.Models;
using UniversityAPI.Services.Exceptions;
using UniversityAPI.Services.Interfaces;

namespace UniversityAPI.Services
{
    /// <summary>
    /// Repository for operation over students data set.
    /// </summary>
    public class StudentRepository : BaseRepository, IStudentRepository
    {
        public StudentRepository(EduDataContext context)
        {
            this.context = context;
        }

        public void AddStudent(Students st)
        {
            context.Students.Add(st);
        }

        public PagedList<Students> GetStudents(StudentQueryParameters stParam)
        {
            var collection = context.Students.AsQueryable();
            if (!string.IsNullOrWhiteSpace(stParam.Id) && int.TryParse(stParam.Id, out var intId))
                collection = collection.Where(st => st.StudentId == intId);

            if (!string.IsNullOrWhiteSpace(stParam.LastName))
                collection = collection.Where(st => st.LastName.Contains(stParam.LastName, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(stParam.FirstName))
                collection = collection.Where(st => st.FirstName.Contains(stParam.FirstName, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(stParam.PhoneNumber))
                collection = collection.Where(st => st.PhoneNumber.Contains(stParam.PhoneNumber, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(stParam.Email))
                collection = collection.Where(st => st.Email.Contains(stParam.Email, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(stParam.City))
                collection = collection.Where(st => st.City.Contains(stParam.City, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(stParam.Adress))
                collection = collection.Where(st => st.Adress.Contains(stParam.Adress, StringComparison.OrdinalIgnoreCase));

            collection = ReflectionHelper.PerformSorting<Students>(stParam.OrderBy, collection);

            return PagedList<Students>.Create(collection, stParam.PageNumber, stParam.PageSize);
        }

        public override bool Save()
        {
            try
            {
                return base.Save();
            }
            catch (Exception Ex)
            {
                throw new RepositoryException($"Error saving students repository. Error : {Ex.Message}");
            }            
        }

        public EnumUpdateResult UpdateStudent(Students st)
        {
            if (!context.Students.Contains(st))
            {
                return EnumUpdateResult.EntryNotFound;
            }
            else
            {
                context.Students.Update(st);
                return EnumUpdateResult.Succesfull;
            }
        }
    }
}
