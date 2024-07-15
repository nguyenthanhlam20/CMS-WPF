using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Assessment
    {
        public Assessment()
        {
            Marks = new HashSet<Mark>();
        }

        public int Id { get; set; }
        public string? Type { get; set; }
        public string? Name { get; set; }
        public double? Percent { get; set; }
        public int? CourseId { get; set; }

        public virtual Course? Course { get; set; }
        public virtual ICollection<Mark> Marks { get; set; }
    }
}
