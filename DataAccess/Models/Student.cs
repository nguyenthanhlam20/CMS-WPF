using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Student
    {
        public Student()
        {
            Enrollments = new HashSet<Enrollment>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Department { get; set; }

        public virtual Department? DepartmentNavigation { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
