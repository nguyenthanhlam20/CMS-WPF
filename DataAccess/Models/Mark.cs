using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Mark
    {
        public int EnrollmentId { get; set; }
        public int AssessmentId { get; set; }
        public decimal? Mark1 { get; set; }

        public virtual Assessment Assessment { get; set; } = null!;
        public virtual Enrollment Enrollment { get; set; } = null!;
    }
}
