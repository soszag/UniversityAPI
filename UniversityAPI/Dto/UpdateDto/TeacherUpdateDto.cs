using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UniversityAPI.Dto.CreationDto;

namespace UniversityAPI.Dto.UpdateDto
{
    public class TeacherUpdateDto : BasePersonCreationDto
    {
        [Required]
        public int TeacherId { get; set; }
    }
}
