using Admission.Core.Domain.Entities;
using Admission.Core.Domain.IdentityEntities;
using Admission.Core.DTO;
using Admission.Core.ServiceContracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Admission.UI.Controllers
{
    public class StudentController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IStudentsService _studentService;
		private readonly IFilesService _filesService;
		
		public StudentController(UserManager<ApplicationUser> userManager,IStudentsService studentsService, IFilesService filesService)
		{
			_userManager = userManager;
			_studentService = studentsService;
			_filesService = filesService;
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
					return ViewComponent("UserDetails", new { action = "ClassList" }); 
				case "MyInformation":
					StudentInfoDTO? studentInfo = await _studentService.GetUserInfoAsync(userId);
					ProfileInfo profileInfo = new ProfileInfo();
					profileInfo.studentInfo = studentInfo;
					if(studentInfo == null)
					{
						return Content("Không có thông tin");
					}
					return ViewComponent("StudentInfo", new { profileInfo }); 
				default:
					return NotFound();
			}
		}
		[HttpPost]
		public async Task<IActionResult> UpdateStudentInfo(StudentUpdateInfoDTO student)
		{
            string? accountID = User.FindFirst("AccountID")?.Value;
			await _studentService.UpdateStudentInfo(student,Guid.Parse(accountID));
            var result = new { res = "Success" };
			return Ok();
		}

        [HttpPost]
        public async Task<IActionResult> UpdateStudyProcess(StudyProcessDTO student)
        {
            string? accountID = User.FindFirst("AccountID")?.Value;
            await _studentService.UpdateStudyInfo(student, accountID);
            var result = new { res = "Success" };
            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> UpdateAnotherData(AnotherStudentDTO student)
        {

            string? accountID = User.FindFirst("AccountID")?.Value;
			Student existStudent = await _studentService.GetStudentByAccountID(Guid.Parse(accountID));

            if (student.CD.ContentType == "image/png")
			{
				IFormFile[] formFiles = new IFormFile[] { student.CD };
				await _studentService.SaveMediaStudent(formFiles, existStudent.StudentID, "CD");
			}
			if(student.KS.ContentType == "image/png")
			{
                IFormFile[] formFiles = new IFormFile[] { student.KS };
                await _studentService.SaveMediaStudent(formFiles, existStudent.StudentID, "KS");
            }
            if (student.NVQS.ContentType == "image/png")
            {
                IFormFile[] formFiles = new IFormFile[] { student.NVQS };
                await _studentService.SaveMediaStudent(formFiles, existStudent.StudentID, "NVQS");
            }
            if (student.TN.ContentType == "image/png")
            {
                IFormFile[] formFiles = new IFormFile[] { student.TN };
                await _studentService.SaveMediaStudent(formFiles, existStudent.StudentID, "TN");
            }
            var result = new { res = "Success" };
            return Ok();
        }
    }
}
