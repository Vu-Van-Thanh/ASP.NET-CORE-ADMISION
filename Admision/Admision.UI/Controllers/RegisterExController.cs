using Microsoft.AspNetCore.Mvc;
using Admission.Core.DTO;

namespace Admission.UI.Controllers
{
    public class RegisterExController : Controller
    {
        [HttpGet]
        public IActionResult register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult register(RegisterExDTO registerExDTO)
        {
            return View();
        }
    }
}
