using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.DTO
{
    public class InformationApplyDTO
    {
        public Guid StudentID { get; set; }
        public string? AdmissionMethod { get; set; }
        public Guid MajorID { get; set; }
        public string? ExamPeriod { get; set; }
        public double GPA10 { get; set; }

        public double GPA11 { get; set; }

        public double GPA12 { get; set; }
    }
}
