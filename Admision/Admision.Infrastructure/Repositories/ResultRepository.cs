using Admission.Core.Domain.Entities;
using Admission.Core.Domain.RepositoryContracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Infrastructure.Repositories
{
    public class ResultRepository : IResultRepository
    {
        private readonly ApplicationDbContext _db;

        public ResultRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<double?> GetResultByAI(Guid Id)
        {
            Result? result = await _db.Results.FirstOrDefaultAsync(r => r.InfoAppliedID == Id);
            if (result == null) return null;

            return result.Score;
        }
    }
}
