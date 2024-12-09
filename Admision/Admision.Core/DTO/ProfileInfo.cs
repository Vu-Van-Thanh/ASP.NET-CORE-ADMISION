using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.DTO
{
    public class ProfileInfo
    {
        public StudentInfoDTO? studentInfo { get; set; }
        public StudentUpdateInfoDTO? studentUpdateInfo { get; set; }
    }
}
