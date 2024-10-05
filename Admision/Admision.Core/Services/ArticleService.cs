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
            string? content = article.Content;
            if (content != null)
            {
                foreach (Media media in article.medias)
                {
                    string mediaTag = string.Empty;

                    // Kiểm tra URL có truy cập được không
                    HttpClient httpClient = new HttpClient();
                    HttpResponseMessage response = await httpClient.GetAsync(media.MediaUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // Nếu URL truy cập được, sử dụng URL
                        if (media.MediaType == "image")
                        {
                            mediaTag = $"<img src='{media.MediaUrl}' alt='Image {media.MediaID}' />";
                        }
                        else if (media.MediaType == "video/mp4")
                        {
                            mediaTag = $"<video controls><source src='{media.MediaUrl}' type='video/mp4'>Your browser does not support the video tag.</video>";
                        }
                    }
                    else
                    {
                        // Nếu không truy cập được URL, sử dụng content lưu trữ trong cơ sở dữ liệu (dạng byte array)
                        if (!string.IsNullOrEmpty(media.MediaContent))
                        {
                            if (media.MediaType == "image")
                            {
                                mediaTag = $"<img src='data:image/jpeg;base64,{media.MediaContent}' alt='Image {media.MediaID}' />";
                            }
                            else if (media.MediaType == "video/mp4")
                            {
                                mediaTag = $"<video controls><source src='data:video/mp4;base64,{media.MediaContent}' type='video/mp4'>Your browser does not support the video tag.</video>";
                            }
                        }
                        else
                        {
                            // Nếu cả URL và media content đều không khả dụng
                            mediaTag = $"<p>Media {media.MediaID} không khả dụng.</p>";
                        }
                    }

                    // Thay thế [media:MediaID] trong content bằng media tag
                    content = content.Replace($"[media:{media.MediaID}]", mediaTag);
                }

                return content;
            }
            else
            {
                return "<p>Không tồn tại thông tin</p>";
            }
        }

        

    }
}
