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
	public class StudentsRepository:IStudentsRepository
	{
		private readonly ApplicationDbContext _dbcontext;
		private readonly UserManager<ApplicationUser> _userManager;
		public StudentsRepository(ApplicationDbContext dbcontext, UserManager<ApplicationUser> userManager) 
		{
			_dbcontext = dbcontext;
			_userManager = userManager;
		}

		public async Task<ApplicationUser?> GetUserAsync(string accountID)
		{
			ApplicationUser? user = await _userManager.FindByIdAsync(accountID);
			if (user == null)
			{
				return null;
			}
			return user;


		}

		public async Task<Student?> GetStudentByAccountID(Guid accountID)
		{
			return await _dbcontext.Students
						.Include(s=>s.ApplicationUser)
						.FirstOrDefaultAsync(s=>s.ApplicationUser.Id == accountID);
		}

		public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
		{
			ApplicationUser? user = await _userManager.FindByEmailAsync(email);
			return user;
		}

		public async Task UpdateUserAsync(ApplicationUser user, Student student)
		{
			_dbcontext.Update(student);
			await _userManager.UpdateAsync(user);

			await _dbcontext.SaveChangesAsync();

		}

		// auhor id = student id
        public async Task<Student> GetAuthorByAuthorId(Guid authorID)
        {
            return await _dbcontext.Students
						.Where(s=>s.StudentID == authorID)
						.FirstOrDefaultAsync();
        }
    }
}
