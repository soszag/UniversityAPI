using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAPI.Dto
{
    /// <summary>
    /// Base type for all non-creational DTO object.
    /// </summary>
    public class BaseTypeDto
    {
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
