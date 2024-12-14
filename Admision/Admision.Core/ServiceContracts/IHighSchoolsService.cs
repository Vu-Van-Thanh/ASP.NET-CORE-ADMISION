using Admission.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.ServiceContracts
{
	public interface IHighSchoolsService
	{
		Task<List<HighSchool>?> GetAllHighSchools();
		Task<HighSchool> GetHighSchoolById(Guid Id);

    }
}
