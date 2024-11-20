using Admission.Core.ServiceContracts;
using Admission.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Admission.UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class MajorController : Controller
	{
		private readonly IMajorsService _majorsService;
		public MajorController(IMajorsService majorService)
		{
			_majorsService = majorService;
		}

		[HttpGet]
		public IActionResult UpLoadProgramDetail ()
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
	}
}
