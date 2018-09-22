using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAPI.Helpers;
using UniversityAPI.Helpers.QueryParameters;
using UniversityAPI.Models;

namespace UniversityAPI.Services.Interfaces
{
    public interface ITeacherRepository : IBaseRepository
    {
        PagedList<Teachers> GetTeachers(TeacherQueryParameters teacherResourceParameters);

        void AddTeacher(Teachers techer);

        EnumUpdateResult UpdateTeacher(Teachers teacher);
    }
}
