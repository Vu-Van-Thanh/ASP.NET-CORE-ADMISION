using Microsoft.AspNetCore.Mvc;

namespace Admission.UI.Controllers
{
    public class PostController : Controller
    {

        [HttpPost]
        public async Task<IActionResult> AddPost()
        {
            return View();
        }
    }
}
