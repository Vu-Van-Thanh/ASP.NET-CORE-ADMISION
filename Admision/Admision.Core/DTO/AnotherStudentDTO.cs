using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.DTO
{
    public class AnotherStudentDTO
    {
        public IFormFile? TN { get; set; }
        public IFormFile? KS { get; set; }
        public IFormFile? CD { get; set; }
        public IFormFile? NVQS {get; set; }
    }
}
