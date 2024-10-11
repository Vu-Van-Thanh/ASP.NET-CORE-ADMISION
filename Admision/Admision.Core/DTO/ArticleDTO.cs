using Admission.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.DTO
{
	public class ArticleDTO
	{
		public Guid ArticleID { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime DateCreate { get; set; }
	}
	public static class ToArticle
	{
		public static ArticleDTO ToArticleDTO(this Article article)
		{
			ArticleDTO articleVM = new ArticleDTO();	
			articleVM.ArticleID = article.ArticleId;
			articleVM.Title = article.Title;
			articleVM.Content = article.Content;
			articleVM.DateCreate = article.DateCreated;
			return articleVM;
		}
	}

}
