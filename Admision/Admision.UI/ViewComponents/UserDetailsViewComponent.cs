using Admission.Core.Domain.Entities;
using Admission.Core.DTO;
using Admission.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace Admission.UI.ViewComponents
{
    public class UserDetailsViewComponent : ViewComponent
    {
        private readonly IInfoAppliesService _infoAppliesService;
        private readonly IStudentsService _studentsService;

        public UserDetailsViewComponent(IInfoAppliesService infoAppliesService, IStudentsService studentsService)
        {
            _infoAppliesService = infoAppliesService;
            _studentsService = studentsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string accountID = User.FindFirst()
            return View();
        }
    }
}
