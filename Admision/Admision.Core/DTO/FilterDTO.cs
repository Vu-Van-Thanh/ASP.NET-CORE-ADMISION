using Admission.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.DTO
{
    public class FilterDTO
    {
        public SalaryResult? WageResult { get; set; }
        public string? Province { get; set; }
    }
}
