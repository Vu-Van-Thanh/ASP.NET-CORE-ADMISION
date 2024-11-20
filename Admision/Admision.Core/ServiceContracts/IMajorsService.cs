using Admission.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.ServiceContracts
{
	public interface IMajorsService
	{
		Task<Major?> GetMajorByMajorID(Guid majorID);
		Task<List<Major>?> GetAllMajors();
		Task UploadFile(IEnumerable<(Stream fileStream, string fileName)> files);
	}
}
