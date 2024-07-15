using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Semester
    {
        public Semester()
        {
            Enrollments = new HashSet<Enrollment>();
        }

        public int Id { get; set; }
        public string? Code { get; set; }
        public int? Year { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
