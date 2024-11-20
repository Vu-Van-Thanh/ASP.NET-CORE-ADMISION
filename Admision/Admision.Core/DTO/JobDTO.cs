using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.DTO
{
    public class JobDTO
    {
        public string? Link { get; set; }
        public string? Web {  get; set; }
        public string? Major { get; set; }
        public string? JobName { get; set; }
        public string? Company { get; set; }
        public string? Province { get; set; }
        public string? Wage {  get; set; }
        public decimal? MinWage {  get; set; }
        public decimal? MaxWage {  get; set; }

        public string? Type { get; set; }
        public string? YOE {  get; set; }
        public string? Level { get; set; }
        public string? DeadlineSubmit { get; set; }
        public string? Requirement { get; set; }
        public string? JobDescription { get; set; }
        public string? Welfare { get; set; }
        public string? Amount { get; set; }
        public string? Image { get; set; }
    }
}
