using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAPI.Dto.UpdateDto
{
    public class ClassUpdateDto
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int ClassId { get; set; }

        /// <summary>
        /// Name of the class
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of this class
        /// </summary>
        public string Description { get; set; }
    }
}
