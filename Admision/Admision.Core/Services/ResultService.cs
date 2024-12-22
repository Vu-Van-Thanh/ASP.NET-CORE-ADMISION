using Admission.Core.Domain.RepositoryContracts;
using Admission.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Services
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository _resultRepository;

        public ResultService(IResultRepository resultRepository)
        {
            _resultRepository = resultRepository;
        }

        public async Task<double?> GetScroreExam(Guid AppliedID)
        {
            double? result = await _resultRepository.GetResultByAI(AppliedID);
            if (result == null) return null;
            return result;
        }
    }
}
