using Admission.Core.Domain.Entities;
using Admission.Core.Domain.RepositoryContracts;
using Admission.Core.ServiceContracts;
using Aspose.Words;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Services
{
	public class MajorService : IMajorsService
	{
		private readonly IMajorsRepository _majorsRepository;
		public MajorService(IMajorsRepository majorsRepository)
		{
			_majorsRepository = majorsRepository;
		}
		public async Task<Major?> GetMajorByMajorID(Guid majorID)
		{
			Major? major = await _majorsRepository.GetMajorByMajorID(majorID);
			return major;
		}
		public async Task<List<Major>?> GetAllMajors()
		{
			List<Major>? list = await _majorsRepository.GetAllMajors();
			return list;
		}
		public async Task UploadFile(IEnumerable<(Stream fileStream, string fileName)> files)
		{
			string year = DateTime.Now.Year.ToString();

			// Đường dẫn thư mục root: wwwroot/Programs/{year}
			string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Programs", year);

			// Tạo thư mục nếu chưa tồn tại
			if (!Directory.Exists(folderPath))
				Directory.CreateDirectory(folderPath);

			foreach (var file in files)
			{
				// Đường dẫn đầy đủ của file
				string filePath = Path.Combine(folderPath, file.fileName);

				// Ghi dữ liệu từ stream vào file
				using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
				{
					await file.fileStream.CopyToAsync(fileStream);
				}
			}
		}
	}
}
