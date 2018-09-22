using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAPI.Dto.CreationDto
{
    public class UserCreationDto : BasePersonCreationDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsTeacher { get; set; }
        public bool IsStudent { get; set; }
        public bool IsParent { get; set; }
    }
}
