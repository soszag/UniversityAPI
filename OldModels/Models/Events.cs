using System;
using System.Collections.Generic;

namespace UniversityAPI.Models
{
    public partial class Events
    {
        public Events()
        {
            Grades = new HashSet<Grades>();
            GradesHistory = new HashSet<GradesHistory>();
        }

        public int EventId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DateWhen { get; set; }
        public int? SubjectId { get; set; }
        public int? ClassId { get; set; }
        public int TeacherId { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public int? ModifiedBy { get; set; }

        public Classes Class { get; set; }
        public Subjects Subject { get; set; }
        public Teachers Teacher { get; set; }
        public ICollection<Grades> Grades { get; set; }
        public ICollection<GradesHistory> GradesHistory { get; set; }
    }
}
