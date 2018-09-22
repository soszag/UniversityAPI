using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAPI.Dto.CreationDto
{
    public class ClassCreationDto
    {
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
