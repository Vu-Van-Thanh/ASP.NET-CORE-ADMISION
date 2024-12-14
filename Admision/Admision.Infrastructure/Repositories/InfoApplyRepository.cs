using Admission.Core.Domain.Entities;
using Admission.Core.Domain.RepositoryContracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Infrastructure.Repositories
{
    public class InfoApplyRepository : IInfoApplyRepository
    {
        private readonly ApplicationDbContext _db;

        public InfoApplyRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<int> AddInfoApply(InformationOfApplied informationOfApplied)
        {
            _db.InformationOfApplieds.Add(informationOfApplied);
            int result = await _db.SaveChangesAsync();
            return result;
        }
    }
}
