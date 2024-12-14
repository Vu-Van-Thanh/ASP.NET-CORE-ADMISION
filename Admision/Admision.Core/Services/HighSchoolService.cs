using Admission.Core.Domain.Entities;
using Admission.Core.Domain.IdentityEntities;
using Admission.Core.Domain.RepositoryContracts;
using Admission.Core.DTO;
using Admission.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Services
{
	public class HighSchoolService : IHighSchoolsService
	{
		private readonly IHighSchoolsRepository _highSchoolsRepository;

		public HighSchoolService(IHighSchoolsRepository highSchoolsRepository)
		{
			_highSchoolsRepository = highSchoolsRepository;
		}

		public async Task<List<HighSchool>?> GetAllHighSchools()
		{
			return await _highSchoolsRepository.GetAllHighSchools();
		}
		public async Task<HighSchool> GetHighSchoolById(Guid Id)
		{
			return await _highSchoolsRepository.GetHighSchoolById(Id);
		}
	}
}
