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

		public async Task<IActionResult> AllArticle(string title, string subTitle, int page = 1, int pageSize = 5)
		{
			if (string.IsNullOrEmpty(title))
			{
				title = "Thông tin chung";
			}

			List<Article> articles = await _articlesService.GetAllArticle();

			// Phân trang
			var pagedArticles = articles
				.OrderByDescending(a => a.DateCreated)  
				.Skip((page - 1) * pageSize)  // Bỏ qua bài viết trên các trang trước
				.Take(pageSize)  // Lấy số bài viết theo pageSize
				.ToList();

			List<ArticleDTO> list = new List<ArticleDTO>();
			foreach (Article article in pagedArticles)
			{
				article.Content = await _articlesService.RenderArticleContent(article);
				list.Add(article.ToArticleDTO());
			}

			ViewBag.Title = title;
			ViewBag.CurrentPage = page;
			ViewBag.TotalPages = (int)Math.Ceiling((double)articles.Count / pageSize);

			return View(list);
		}



		public async Task<IActionResult> ShowArticleDetails(Guid ArticleID)
        {
            Article? article = await _articlesService.GetArticleByArticleID(ArticleID);
            ViewBag.Title = article?.Title;
            article.Content = await _articlesService.RenderArticleContent(article);
            return View(article.ToArticleDTO());
        }
    }
}
