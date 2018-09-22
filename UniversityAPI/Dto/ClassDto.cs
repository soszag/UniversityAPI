using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAPI.Dto
{
    public class ClassDto : BaseTypeDto
    {
        public int ID { get; set; }

        /// <summary>
        /// Name of the class
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of this class
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Collection of subject and assigned teachers for current class.
        /// </summary>
        public IReadOnlyDictionary<TeacherDto, SubjectDto> TeacherSubjectForCurrentClass { get; set; }

        /// <summary>
        /// Collection of all students belongs to current class.
        /// </summary>
        public IReadOnlyCollection<StudentDto> Students { get; set; }
    }
}
