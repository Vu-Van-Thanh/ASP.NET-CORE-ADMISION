using Admission.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.ServiceContracts
{   public interface IArticlesService
    {
        Task<Article?> GetArticleByArticleID(Guid articleID);
        Task<List<Article>?> GetAllArticle();
        Task ExtractNewsFromStream(Stream stream, string filepath);
        Task<string> RenderArticleContent(Article article);
    }
}
