using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.ServiceContracts
{
    public interface IResultService
    {
        Task<double?> GetScroreExam(Guid AppliedID);
    }
}
