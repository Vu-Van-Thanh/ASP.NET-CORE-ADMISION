using Admission.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Domain.RepositoryContracts
{
     public interface IRelativesRepository
    {
        Task<List<Relative>?> GetRelativeByStudentId(Guid studentId);
        Task<Relative> AddRelative(Relative relative);
    }
}
