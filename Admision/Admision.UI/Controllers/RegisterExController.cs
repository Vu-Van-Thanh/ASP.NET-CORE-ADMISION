using Microsoft.AspNetCore.Mvc;
using Admission.Core.DTO;

namespace Admission.UI.Controllers
{
    public class RegisterExController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RegisterEx(RegisterExDTO registerExDTO)
        {
            return View();
		}
    }
}
