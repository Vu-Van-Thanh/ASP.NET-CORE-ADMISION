using Admission.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Domain.RepositoryContracts
{
    public interface IArticlesRepository
    {
        Task<Article?> GetArticleByArticleID (Guid articleID);
        Task<List<Article>?> GetAllArticle();

        Task<Article> AddArticle(Article article);
        Task<Article?> GetArticleByType(string articleType);
        Task UpdateArticle(Article article);
    }
}
