using Microsoft.AspNetCore.Mvc;

namespace Admission.UI.Controllers
{
    public class Test : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
