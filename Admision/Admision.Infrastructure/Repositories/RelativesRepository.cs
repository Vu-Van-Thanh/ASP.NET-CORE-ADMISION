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
    public class RelativesRepository : IRelativesRepository
    {
        private readonly ApplicationDbContext _db;

        public RelativesRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Relative> AddRelative(Relative relative)
        {
             _db.Relatives.Add(relative);
            await _db.SaveChangesAsync();
            return relative;
        }

        public async Task<List<Relative>?> GetRelativeByStudentId(Guid studentId)
        {
            List<Relative>? relatives = await _db.Relatives
                                            .Where(re=> re.StudentID == studentId).ToListAsync();
            return relatives;
        }
    }
}
