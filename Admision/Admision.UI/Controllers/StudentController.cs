using Admission.Core.Domain.Entities;
using Admission.Core.Domain.IdentityEntities;
using Admission.Core.DTO;
using Admission.Core.ServiceContracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Admission.UI.Controllers
{
    public class StudentController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IStudentsService _studentService;
		public StudentController(UserManager<ApplicationUser> userManager,IStudentsService studentsService)
		{
			_userManager = userManager;
			_studentService = studentsService;
		}

		[HttpGet]
		public async Task<IActionResult> LoadPersonalPage(Guid accountID)
        {
			ApplicationUser?  user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return Unauthorized();
			}
			else
			{
				Student? student = await _studentService.GetStudentByAccountID(accountID);
				if (student == null)
				{
					return NotFound("Không tìm thấy thông tin người dùng!");
				}
				else
				{
					if (student.ApplicationUser.Id != user.Id)
					{
						return Unauthorized();
					}
					else
					{
						return View(student);
					}
				}
				
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetDetail(string option, string userId) // Nhận userId
		{
			switch (option)
			{
				case "ClassList":
					return ViewComponent("UserDetails", new { action = "ClassList" }); // Gọi View Component
				case "MyInformation":
					StudentInfoDTO? studentInfo = await _studentService.GetUserInfoAsync(userId);
					if(studentInfo == null)
					{
						return Content("Không có thông tin");
					}
					return ViewComponent("StudentInfo", new { studentInfo }); // Gọi View Component
				default:
					return NotFound();
			}
		}

		[HttpPost]
		public async Task<IActionResult> UpdateStudentInfo(StudentInfoDTO studentInfo, IFormFile pathOfAvatar)
		{
			// Xử lý upload ảnh nếu có
			if (pathOfAvatar != null)
			{
				// Lưu ảnh vào thư mục cần thiết
				var filePath = Path.Combine("wwwroot/avataruser", pathOfAvatar.FileName);
				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await pathOfAvatar.CopyToAsync(stream);
				}
				studentInfo.PathOfAvatar = pathOfAvatar.FileName; // Lưu tên file vào DTO
			}

			// Cập nhật thông tin sinh viên trong cơ sở dữ liệu
			await _studentService.UpdateStudentInfoAsync(studentInfo);

			return RedirectToAction("Index", "Home");
		}




	}
}
