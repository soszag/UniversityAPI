using System;
using System.Collections.Generic;

namespace UniversityAPI.Models
{
    public partial class GradeDefinitions
    {
        public GradeDefinitions()
        {
            Grades = new HashSet<Grades>();
            GradesHistory = new HashSet<GradesHistory>();
        }

        public int GradeDefinitionId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Tag { get; set; }
        public string Description { get; set; }
        public decimal? Value { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public int? ModifiedBy { get; set; }

        public ICollection<Grades> Grades { get; set; }
        public ICollection<GradesHistory> GradesHistory { get; set; }
    }
}
