using Admission.Core.Domain.Entities;
using Admission.Core.ServiceContracts;
using Admission.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Admission.UI.Controllers
{
    public class MajorController : Controller
    {
        private readonly IMajorsService _majorsService;

		public MajorController(IMajorsService majorsService)
		{
			_majorsService = majorsService;
		}
		public async Task<IActionResult> AcademicProgram()
        {
            IEnumerable<Major> list = await _majorsService.GetAllMajors();
            return View(list);
        }
        public async Task<IActionResult> ProgramDetail(Guid ProgramID)
        {
			Major? major = await _majorsService.GetMajorByMajorID(ProgramID);
			ViewBag.Title = major?.Name;
			return View();
		}
    }
}
