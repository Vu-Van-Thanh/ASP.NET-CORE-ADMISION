using Admission.Core.Domain.Entities;
using Admission.Core.Domain.IdentityEntities;
using Admission.Core.Domain.RepositoryContracts;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Infrastructure.Repositories
{
	public class HighSchoolsRepository : IHighSchoolsRepository
	{
		private readonly ApplicationDbContext _dbcontext;

		public HighSchoolsRepository(ApplicationDbContext dbcontext)
		{
			_dbcontext = dbcontext;
		}

		public async Task<List<HighSchool>?> GetAllHighSchools()
		{
			return await _dbcontext.HighSchools.ToListAsync();
		}

        public async Task<HighSchool> GetHighSchoolById(Guid Id)
        {
            return await _dbcontext.HighSchools.FirstOrDefaultAsync(h => h.HighSchoolID == Id);
        }
    }
}
