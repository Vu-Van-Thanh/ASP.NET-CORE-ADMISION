using Admission.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.ServiceContracts
{
    public interface IRecruitDataService
    {
        Task<List<JobDTO>> GetJob();
        Task<object> GetJobStatic();
    }
}
