using Admission.Core.Domain.Entities;
using Admission.Core.DTO;
using Admission.Core.ServiceContracts;
using Admission.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Admission.UI.Controllers
{
    public class RelativeController : Controller
    {
        private readonly IStudentsService _studentsService;
        private readonly IRelativeService _relativesService;

        public RelativeController(IStudentsService studentsService, IRelativeService relativesService)
        {
            _studentsService = studentsService;
            _relativesService = relativesService;   
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRelative(RelativeDTO relative)
        {
            string? accountId = User.FindFirst("AccountID").Value;
            Student student = await _studentsService.GetStudentByAccountID(Guid.Parse(accountId));

            await _relativesService.AddRelative(relative,student.StudentID);
            var res = new { result = "Success" };
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFamiliar(FamiliarDTO familiar)
        {
            string? accountID = User.FindFirst("AccountID")?.Value;
            Student existStudent = await _studentsService.GetStudentByAccountID(Guid.Parse(accountID));
            var relativeJson = Request.Form["Relative"];
            if (!string.IsNullOrEmpty(relativeJson))
            {
                familiar.Relative = System.Text.Json.JsonSerializer.Deserialize<List<RelativeShort>>(relativeJson);
            }
            await _studentsService.SaveMediaStudent(familiar.HKImage, existStudent.StudentID, "HK");
            if (familiar.Relative != null)
            {
                await _relativesService.UpdateRelativeShort(familiar.Relative, existStudent.StudentID);
            }
            Student student = new Student();
            student.StudentID = existStudent.StudentID; 
            student.PolicySubjectType = familiar.PolicySubjectType;
            student.HouseholdType = familiar.HouseholdType;
            /*student.JoiningDate = null;*/
            await _studentsService.AddAdditionalInformation(student);
            var resul = new { Succ = "oke" };
            return Ok(resul);
        }
    }
}
