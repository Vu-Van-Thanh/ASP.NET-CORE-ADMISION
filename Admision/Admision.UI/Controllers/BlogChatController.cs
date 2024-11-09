using Admission.Core.DTO;
using Admission.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace Admission.UI.Controllers
{
    public class BlogChatController : Controller
    {
        private readonly IGroupsService _groupsService;

        public BlogChatController(IGroupsService groupsService)
        {
            _groupsService = groupsService;
        }

        public async Task<IActionResult> AllGroup()
        {
            List<GroupDTO> groups = (await _groupsService.GetAllGroups()).ToList();
            return View(groups);
        }
    }
}
