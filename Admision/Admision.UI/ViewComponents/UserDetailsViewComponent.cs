using Admission.Core.Domain.Entities;
using Admission.Core.DTO;
using Admission.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Admission.UI.ViewComponents
{
    public class UserDetailsViewComponent : ViewComponent
    {
        private readonly IInfoAppliesService _infoAppliesService;
        private readonly IStudentsService _studentsService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserDetailsViewComponent(IInfoAppliesService infoAppliesService, IStudentsService studentsService, IHttpContextAccessor httpContextAccessor)
        {
            _infoAppliesService = infoAppliesService;
            _studentsService = studentsService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            string? accountId = user?.FindFirst("AccountID")?.Value;
            Student? student = await _studentsService.GetStudentByAccountID(Guid.Parse(accountId));
            List<ExamClassDTO> list = await _infoAppliesService.GetInfoApplyByStudentId(student.StudentID.ToString());
            return View(list);
        }
    }
}
