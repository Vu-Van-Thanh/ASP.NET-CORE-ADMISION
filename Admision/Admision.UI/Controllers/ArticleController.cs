using Admission.Core.Domain.Entities;
using Admission.Core.Domain.RepositoryContracts;
using Admission.Core.ServiceContracts;
using Admission.UI.ViewModels;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Admission.UI.ViewModels.ArticleVM;

namespace Admission.UI.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IArticlesService _articlesService;
        public ArticleController(IArticlesService articlesService, ApplicationDbContext context)
        {
            _articlesService = articlesService;
            _context = context;
        }

        public async Task<IActionResult> Index(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                title = "Thông tin chung";
            }

            /*var articles = await _context.Articles
									 .Select(a => new ArticleVM
									 {
										 ArticleID = a.ArticleId,
										 Title = a.Title,
										 Content = a.Content,
										 DateCreate = a.DateCreated,
									 })
									 .ToListAsync();*/

            var articles = new List<ArticleVM>
{
    new ArticleVM { ArticleID = Guid.NewGuid(), Title = "Bài viết 1", Content = "Nội dung bài viết 1", DateCreate = DateTime.Now },
    new ArticleVM { ArticleID = Guid.NewGuid(), Title = "Bài viết 2", Content = "Nội dung bài viết 2", DateCreate = DateTime.Now.AddDays(-1) }
};
            ViewBag.Title = title;
            return View(articles);
        }
        public async Task<IActionResult> ShowArticle()
        {
            List<Article>? list = await _articlesService.GetAllArticle();
            return View(list);
        }
    }
}
