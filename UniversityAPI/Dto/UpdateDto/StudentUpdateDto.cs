using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UniversityAPI.Dto.CreationDto;

namespace UniversityAPI.Dto.UpdateDto
{
    public class StudentUpdateDto : BasePersonCreationDto
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int StudentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ClassId { get; set; }
    }
}
