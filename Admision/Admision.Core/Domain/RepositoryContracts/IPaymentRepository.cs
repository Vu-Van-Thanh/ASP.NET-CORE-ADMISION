using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Domain.RepositoryContracts
{
    public interface IPaymentRepository
    {
        Task<Guid?> GetPaymetApplied(Guid AppliedID);
    }
}
