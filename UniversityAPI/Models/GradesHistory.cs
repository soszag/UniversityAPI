using System;
using System.Collections.Generic;

namespace UniversityAPI.Models
{
    public partial class GradesHistory
    {
        public int GradeHistoryId { get; set; }
        public DateTime ModificationDate { get; set; }
        public int GradeId { get; set; }
        public int StudentId { get; set; }
        public int? SubjectId { get; set; }
        public int? EventId { get; set; }
        public int? GradeDefinitionId { get; set; }
        public int TeacherId { get; set; }
        public DateTime? DateWhen { get; set; }
        public decimal? Points { get; set; }
        public decimal? Percents { get; set; }
        public decimal? Weight { get; set; }

        public Events Event { get; set; }
        public Grades Grade { get; set; }
        public GradeDefinitions GradeDefinition { get; set; }
        public Students Student { get; set; }
        public Subjects Subject { get; set; }
        public Teachers Teacher { get; set; }
    }
}
