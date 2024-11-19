using Admission.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Domain.RepositoryContracts
{
	public interface IMajorsRepository
	{
		Task<Major?> GetMajorByMajorID(Guid majorID);
		Task<List<Major>?> GetAllMajors();

		Task<Major> AddMajor(Major major);
	}
}
