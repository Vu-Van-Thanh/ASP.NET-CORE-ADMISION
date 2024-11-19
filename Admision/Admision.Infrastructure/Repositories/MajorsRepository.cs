using Admission.Core.Domain.RepositoryContracts;
using Admission.Core.Domain.Entities;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Infrastructure.Repositories
{
	public class MajorsRepository: IMajorsRepository
	{
		private readonly ApplicationDbContext _context;
		public MajorsRepository(ApplicationDbContext context) 
		{
			_context = context;
		}
		public async Task<Major> AddMajor(Major major)
		{
			_context.Majors.Add(major);
			await _context.SaveChangesAsync();
			return major;
		}
		public async Task<List<Major>?> GetAllMajors()
		{
			return await _context.Majors.ToListAsync();
		}
		public async Task<Major?> GetMajorByMajorID(Guid majorID)
		{
			return await _context.Majors.FirstOrDefaultAsync(temp => temp.MajorId == majorID);
		}
	}
}
