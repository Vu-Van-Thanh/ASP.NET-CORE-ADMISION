using Microsoft.AspNetCore.Mvc;
using Admission.Core.DTO;
using Admission.Core.ServiceContracts;
using Admission.Core.Domain.Entities;

namespace Admission.UI.Controllers
{
    public class RegisterExController : Controller
    {
        private readonly IStudentMediasService _studentMediasService;
        private readonly IStudentsService _studentsService;

        public RegisterExController(IStudentMediasService studentMediasService, IStudentsService studentsService)
        {
            _studentMediasService = studentMediasService;
            _studentsService = studentsService;
        }

        [HttpGet]
        public async Task<IActionResult> registerExam()
        {
            string? accountId = User.FindFirst("AccountID").Value;
            Student? student = await _studentsService.GetStudentByAccountID(Guid.Parse(accountId));
            List<StudentMediaDTO> list = await _studentMediasService.GetStudentMediasAsync(student.StudentID);
            RegisterExDTO registerExDTO = new RegisterExDTO();
            return View(registerExDTO);
        }
        [HttpPost]

        public IActionResult registerExam(RegisterExDTO registerExDTO)
        {
            return View();
		    }
    }
}
