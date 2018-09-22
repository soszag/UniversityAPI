using System;
using System.Collections.Generic;

namespace UniversityAPI.Models
{
    public partial class Classes
    {
        public Classes()
        {
            Events = new HashSet<Events>();
            Students = new HashSet<Students>();
            TeacherSubject = new HashSet<TeacherSubject>();
        }

        public int ClassId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public int? ModifiedBy { get; set; }

        public ICollection<Events> Events { get; set; }
        public ICollection<Students> Students { get; set; }
        public ICollection<TeacherSubject> TeacherSubject { get; set; }
    }
}
