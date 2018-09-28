using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAPI.Dto.CreationDto
{
    public class UserCreationDto : BasePersonCreationDto
    {
        [MinLength(5, ErrorMessage = "User name must be at least 5 characters length")]
        public string UserName { get; set; }
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$", ErrorMessage = "Password must have minimum eight characters,, at least one letter upper case and one lower case and one number")]
        public string Password { get; set; }

        public bool IsTeacher { get; set; }
        public bool IsStudent { get; set; }
        public bool IsParent { get; set; }
    }
}
