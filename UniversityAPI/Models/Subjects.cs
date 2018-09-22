using System;
using System.Collections.Generic;

namespace UniversityAPI.Models
{
    public partial class Subjects
    {
        public Subjects()
        {
            Events = new HashSet<Events>();
            Grades = new HashSet<Grades>();
            GradesHistory = new HashSet<GradesHistory>();
            TeacherSubject = new HashSet<TeacherSubject>();
        }

        public int SubjectId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public int? ModifiedBy { get; set; }

        public ICollection<Events> Events { get; set; }
        public ICollection<Grades> Grades { get; set; }
        public ICollection<GradesHistory> GradesHistory { get; set; }
        public ICollection<TeacherSubject> TeacherSubject { get; set; }
    }
}
