using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Department
    {
        public Department()
        {
            Students = new HashSet<Student>();
        }

        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;

        public virtual ICollection<Student> Students { get; set; }
    }
}
