using Microsoft.AspNetCore.Mvc;

namespace Admission.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles="Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
