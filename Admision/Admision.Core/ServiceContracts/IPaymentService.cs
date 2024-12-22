using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.ServiceContracts
{
    public interface IPaymentService
    {
        Task<Guid?> GetPaymentByAI(Guid Id); // Id info apply
    }
}
