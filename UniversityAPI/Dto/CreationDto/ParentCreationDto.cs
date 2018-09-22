using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAPI.Dto.CreationDto
{
    public class ParentCreationDto : BasePersonCreationDto
    {
        public string ChildrensIds { get; set; }
    }
}
