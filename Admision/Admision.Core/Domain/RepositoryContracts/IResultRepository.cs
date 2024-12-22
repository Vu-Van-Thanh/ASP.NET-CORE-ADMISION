using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Domain.RepositoryContracts
{
    public interface IResultRepository
    {
        Task<double?> GetResultByAI(Guid Id);
    }
}
