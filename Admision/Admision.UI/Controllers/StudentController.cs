using Admission.Core.Domain.Entities;
using Admission.Core.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Admission.UI.Controllers
{
    public class StudentController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;

		public StudentController(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}

		public IActionResult LoadPersonalPage(Guid studentID)
        {
            return View();
        }

		[HttpGet]
		public ActionResult GetDetail(string action, int userId) // Nhận userId
		{
			switch (action)
			{
				case "ClassList":
					return PartialView("_ClassList"); // Trả về view một phần
				case "MyAccount":
					var userInfo = GetUserInfo(userId); // Lấy thông tin người dùng theo ID
					return PartialView("_MyAccount", userInfo); // Trả về view một phần với model là thông tin người dùng
				case "Voucher":
					return PartialView("_Voucher"); // Trả về view một phần
				default:
					return NotFound();
			}
		}

		private Student GetUserInfo(int userId)
		{
			// Logic để lấy thông tin người dùng từ database hoặc service
			// Giả sử bạn có một lớp User với các thông tin cần thiết
			return new Student
			{
				
			};
		}
	}
}
