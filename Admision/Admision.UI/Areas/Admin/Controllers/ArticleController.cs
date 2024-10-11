using Admission.Core.Domain.RepositoryContracts;
using Admission.Core.ServiceContracts;
using Admission.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admission.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles="Admin")]
    public class ArticleController : Controller
    {
        private readonly IArticlesService _articlesService;
        public ArticleController(IArticlesService articleService)
        {
            _articlesService = articleService;
        }
        [HttpGet]
        public async Task<IActionResult> UploadNews()
        { 
            return View();
        }
            [HttpPost]
        public async Task<IActionResult> UploadNews(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is not selected");
            }

            // Sử dụng stream và truyền tên file vào hàm
            using (var stream = file.OpenReadStream())
            {
                 await _articlesService.ExtractNewsFromStream(stream, file.FileName);
            }

            return View();
        }
    }
}
