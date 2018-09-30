using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAPI.Dto.CreationDto
{
    public class SubjectCreationDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ShortName { get; set; }

        public string Description { get; set; }
    }
}
