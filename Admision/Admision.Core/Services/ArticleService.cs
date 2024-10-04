using Admission.Core.Domain.Entities;
using Admission.Core.Domain.RepositoryContracts;
using Admission.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Services
{
    public class ArticleService:IArticlesService
    {
        private readonly IArticlesRepository _articlesRepository;
        public ArticleService(IArticlesRepository articlesRepository)
        {
            _articlesRepository = articlesRepository;   
        }

        public async Task<List<Article>?> GetAllArticle()
        {
            List<Article>? list = await _articlesRepository.GetAllArticle();
            return list;
        }

        public async Task<Article?> GetArticleByArticleID(Guid articleID)
        {
            Article? article = await _articlesRepository.GetArticleByArticleID(articleID);
            if (article != null)
            {
                return article;
            }
            return new Article() { Title="Tin tức mặc định", Content="Không có thông tin", DateCreated = DateTime.Now};
        }

        public async Task<string> RenderArticleContent(Article article)
        {
            string content = article.Content;
            foreach (Media media in article.medias) { }
            return "ss";
        }
    }
}
