using System;
using System.Collections.Generic;

namespace UniversityAPI.Models
{
    public partial class TeacherSubject
    {
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public int ClassId { get; set; }

        public Classes Class { get; set; }
        public Subjects Subject { get; set; }
        public Teachers Teacher { get; set; }
    }
}
