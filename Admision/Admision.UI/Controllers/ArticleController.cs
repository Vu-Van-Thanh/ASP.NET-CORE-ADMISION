using Microsoft.AspNetCore.Mvc;

namespace Admission.UI.Controllers
{
    public class ArticleController : Controller
    {
        public async Task<IActionResult> Index()
        {
            
            return View();
        }
    }
}
