using Microsoft.AspNetCore.Mvc;

namespace Admission.UI.Controllers
{
	public class AcademicPrograms : Controller
	{

		public IActionResult ProgramList()
		{
			return View();
		}
		public IActionResult ProgramDetail() 
		{
			return View();
		}
	}
}
