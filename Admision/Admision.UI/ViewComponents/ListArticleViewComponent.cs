using Admission.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Admission.Core.DTO;
using Admission.Core.Domain.Entities; 
using System.Collections.Generic;
using Admission.Core.ServiceContracts;

namespace Admission.UI.ViewComponents
{
	public class ListArticleViewComponent : ViewComponent
	{
		private readonly IArticlesService _articleService; 
		public ListArticleViewComponent(IArticlesService articleService)
		{
			_articleService = articleService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{

			var articles = await _articleService.GetAllArticle();

			var articleDTOs = articles.Select(article => article.ToArticleDTO()).ToList();

			return View(articleDTOs);
		}
	}
}
