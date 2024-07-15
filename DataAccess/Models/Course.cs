using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Course
    {
        public Course()
        {
            Assessments = new HashSet<Assessment>();
            Enrollments = new HashSet<Enrollment>();
        }

        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Title { get; set; } = null!;
        public byte? Credits { get; set; }

        public virtual ICollection<Assessment> Assessments { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
