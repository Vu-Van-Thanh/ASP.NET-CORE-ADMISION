using Admission.Core.Domain.Entities;
using Admission.Core.Domain.RepositoryContracts;
using Admission.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace Admission.UI.Controllers
{
    public class ArticleController : Controller
    {

        private readonly IArticlesService _articlesService;
        public ArticleController(IArticlesService articlesService)
        {
            _articlesService = articlesService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> ShowArticle()
        {
            List<Article>? list = await _articlesService.GetAllArticle();
            return View(list);
        }
    }
}
