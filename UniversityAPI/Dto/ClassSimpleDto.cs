using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAPI.Dto
{
    public class ClassSimpleDto
    {
        public int ID { get; set; }

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
