using Admission.Core.Domain.Entities;
using Admission.Core.ServiceContracts;
using Admission.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Admission.UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class MajorController : Controller
	{
		private readonly IMajorsService _majorsService;
		private readonly IArticlesService _articlesService;
        public MajorController(IMajorsService majorService, IArticlesService articlesService)
		{
			_majorsService = majorService;
            _articlesService = articlesService;
		}

		[HttpGet]
		public IActionResult UpLoadProgramDetail()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> UploadProgramDetail(IEnumerable<IFormFile> files)
		{
			if (files == null || !files.Any())
			{
				ViewBag.Message = "No files selected!";
				return View();
			}

			// Chuẩn bị danh sách dữ liệu file để gửi sang service
			var fileData = new List<(Stream fileStream, string fileName)>();

			foreach (var file in files)
			{
				fileData.Add((file.OpenReadStream(), file.FileName));
			}

			// Gọi service để xử lý lưu trữ file
			await _majorsService.UploadFile(fileData);

			ViewBag.Message = $"{files.Count()} file(s) uploaded successfully!";
			return View();
		}


		[HttpGet]
		public async Task<IActionResult> UpdateMajorAdmission()
		{
			List<Major> majors = await _majorsService.GetAllMajors();
			return View(majors);
		}


        [HttpPost]
        public async Task<IActionResult> UpdateMajorAdmission(string year, string selectedMajors)
        {
           
            if (string.IsNullOrWhiteSpace(selectedMajors))
            {
                return BadRequest("Vui lòng chọn ít nhất một ngành học.");
            }

            
            var majors = selectedMajors.Split(',')
                                       .Where(id => !string.IsNullOrWhiteSpace(id))
                                       .Select(Guid.Parse)
                                       .ToList();

            
            List<string> filesToProcess = new List<string>();

           
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","Programs",year);

            // Kiểm tra thư mục có tồn tại không
            if (!Directory.Exists(folderPath))
            {
                return NotFound($"Thư mục cho năm {year} không tồn tại.");
            }

            // Lấy thông tin ngành học từ danh sách `majors`
            foreach (var majorId in majors)
            {
                Major? major = await _majorsService.GetMajorByMajorID(majorId);

                if (major == null || string.IsNullOrWhiteSpace(major.Name))
                {
                    continue; // Bỏ qua nếu không tìm thấy ngành học
                }

                // Tên ngành học
                string majorName = major.Name;

                // Tìm tất cả file Word liên quan trong thư mục (case insensitive)
                string[] files = Directory.GetFiles(folderPath, $"*{majorName}*.docx", SearchOption.TopDirectoryOnly);

                // Thêm các file tìm được vào danh sách
                filesToProcess.AddRange(files);
            }

            // Nếu không tìm thấy file nào
            if (!filesToProcess.Any())
            {
                return NotFound("Không tìm thấy file Word nào liên quan đến các ngành đã chọn.");
            }

            
            foreach (string filePath in filesToProcess)
            {
                try
                {
                   string name = _majorsService.ExtractMajorCode(filePath);
                    await _articlesService.AddOrUpdateArticlesFromFiles(filePath, year, name);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi khi xử lý file {filePath}: {ex.Message}");
                }
            }

            // Trả về thông báo thành công hoặc tiếp tục xử lý khác
            return Ok(new
            {
                Message = "Các file Word đã được xử lý thành công.",
                ProcessedFiles = filesToProcess.Select(f => Path.GetFileName(f)).ToList()
            });
        }


    }
}
