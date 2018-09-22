using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAPI.Dto.CreationDto
{
    public class StudentCreationDto : BasePersonCreationDto
    {
        public int? ClassID { get; set; }
    }
}
