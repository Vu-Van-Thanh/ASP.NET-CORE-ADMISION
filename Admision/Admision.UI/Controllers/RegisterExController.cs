using Microsoft.AspNetCore.Mvc;
using Admission.Core.DTO;

namespace Admission.UI.Controllers
{
    public class RegisterExController : Controller
    {
        [HttpGet]

        public IActionResult registerExam()
        {
            return View();
        }
        [HttpPost]

        public IActionResult registerExam(RegisterExDTO registerExDTO)
        {
            return View();
		}
    }
}
