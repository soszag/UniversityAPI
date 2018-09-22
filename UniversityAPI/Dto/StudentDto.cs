using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAPI.Dto
{
    public class StudentDto : PersonDto
    {
        public int StudentID { get; set; }

        public int? ClassId { get; set; }
    }
}
