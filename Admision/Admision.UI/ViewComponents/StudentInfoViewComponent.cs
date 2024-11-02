using Admission.Core.Domain.Entities;
using Admission.Core.DTO;
using Admission.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace Admission.UI.ViewComponents
{
	public class StudentInfoViewComponent : ViewComponent
	{
		private readonly IHighSchoolsService _highSchoolsService;

		public StudentInfoViewComponent(IHighSchoolsService highSchoolsService)
		{
			_highSchoolsService = highSchoolsService;
		}

		public async Task<IViewComponentResult> InvokeAsync(StudentInfoDTO studentInfo)
		{
			List<HighSchool>? schoolList = await _highSchoolsService.GetAllHighSchools();
			ViewBag.SchoolList = schoolList;
			return View(studentInfo);
		}
	}
}
