using Admission.Core.Domain.Entities;
using Admission.Core.Domain.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Domain.RepositoryContracts
{
	public interface IStudentsRepository
	{
		Task<Student?> GetStudentByAccountID (Guid accountID);
		Task<ApplicationUser?> GetUserAsync (string accountID);
		Task<ApplicationUser?> GetUserByEmailAsync (string email);

		Task UpdateUserAsync(ApplicationUser user,Student student);
		Task <Student> GetAuthorByAuthorId(Guid authorID);

    }
}
