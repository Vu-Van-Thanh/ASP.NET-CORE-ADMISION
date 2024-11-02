using Admission.Core.Domain.Entities;
using Admission.Core.Domain.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Domain.RepositoryContracts
{
	public interface IHighSchoolsRepository
	{
		Task<List<HighSchool>?> GetAllHighSchools();
	}
}
