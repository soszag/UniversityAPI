using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAPI.Dto
{
    public class UserDto : PersonDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }

        public bool IsTeacher { get; set; }
        public bool IsStudent { get; set; }
        public bool IsParent { get; set; }
    }
}
