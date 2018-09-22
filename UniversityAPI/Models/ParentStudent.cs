using System;
using System.Collections.Generic;

namespace UniversityAPI.Models
{
    public partial class ParentStudent
    {
        public int ParentStudentId { get; set; }
        public int? StudentId { get; set; }
        public int? ParentId { get; set; }

        public Parents Parent { get; set; }
        public Students Student { get; set; }
    }
}
