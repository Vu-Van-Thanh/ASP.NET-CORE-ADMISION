using Admission.Core.Domain.Entities;
using Admission.Core.Domain.RepositoryContracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Infrastructure.Repositories
{
    public class ArticlesRepository : IArticlesRepository
    {
        private readonly ApplicationDbContext _db;
        public ArticlesRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Article> AddArticle(Article article)
        {
            _db.Articles.Add(article);
            await _db.SaveChangesAsync();
            return article;
        }

        public async Task<List<Article>?> GetAllArticle()
        {
            return await _db.Articles.Include("medias").ToListAsync();
        }

        public async Task<Article?> GetArticleByArticleID(Guid articleID)
        {
            return await _db.Articles.Include("medias").FirstOrDefaultAsync(temp => temp.ArticleId == articleID);
        }


    }
}
