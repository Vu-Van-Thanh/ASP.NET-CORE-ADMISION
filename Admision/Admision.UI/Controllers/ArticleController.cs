using Admission.Core.Domain.Entities;
using Admission.Core.Domain.RepositoryContracts;
using Admission.Core.DTO;
using Admission.Core.ServiceContracts;
using Admission.UI.ViewModels;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public  async Task<IActionResult> AllArticle(string title,string subTitle)
        {
            if (string.IsNullOrEmpty(title))
            {
                title = "Thông tin chung";
            }
            List<Article> articles = await _articlesService.GetAllArticle();
            List<ArticleDTO> list = new List<ArticleDTO>();
            foreach (Article article in articles)
            {
                list.Add(article.ToArticleDTO());
            }
            ViewBag.Title = title;
            return View(list);
        }
/*        public async Task<IActionResult> ShowArticle()
        {
            List<Article>? list = await _articlesService.GetAllArticle();
            return View(list);
        }*/

        public async Task<IActionResult> ShowArticleDetails(Guid ArticleID)
        {
            Article? article = await _articlesService.GetArticleByArticleID(ArticleID);
            ViewBag.Title = article?.Title;
            return View(article.ToArticleDTO());
        }
    }
}
