using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Enrollment
    {
        public Enrollment()
        {
            Marks = new HashSet<Mark>();
        }

        public int EnrollmentId { get; set; }
        public int? StudentId { get; set; }
        public int? CourseId { get; set; }
        public int? SemesterId { get; set; }

        public virtual Course? Course { get; set; }
        public virtual Semester? Semester { get; set; }
        public virtual Student? Student { get; set; }
        public virtual ICollection<Mark> Marks { get; set; }
    }
}
