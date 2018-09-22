using UniversityAPI.Helpers;
using UniversityAPI.Helpers.QueryParameters;
using UniversityAPI.Models;

namespace UniversityAPI.Services.Interfaces
{
    public interface IStudentRepository : IBaseRepository
    {
        PagedList<Students> GetStudents(StudentQueryParameters studentResourceParameters);

        void AddStudent(Students st);

        EnumUpdateResult UpdateStudent(Students st);
    }
}
