using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAPI.Dto
{
    public class TeacherDto : PersonDto
    {
        public int ID { get; set; }

        //public ICollection<Events> Events { get; set; }
        //public ICollection<Grades> Grades { get; set; }

        //public ICollection<TeacherSubject> TeacherSubject { get; set; }
        //public ICollection<Users> Users { get; set; }
    }
}
