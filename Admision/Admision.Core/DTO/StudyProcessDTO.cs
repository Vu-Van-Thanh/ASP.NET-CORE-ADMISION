using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.DTO
{
    public class StudyProcessDTO
    {
        public string? AcademicPerformance10 { get; set; }
        public string? AcademicPerformance11 { get; set; }
        public string? AcademicPerformance12 { get; set; }
        public string? Conduct10 { get; set; }
        public string? Conduct11 { get; set; }
        public string? Conduct12 { get; set; }
        public string? OutstandingAchievements { get; set; }
        public IFormFile[]? HBImage { get; set; }

    }
}
