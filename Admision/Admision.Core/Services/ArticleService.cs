using Admission.Core.Domain.Entities;
using Admission.Core.Domain.RepositoryContracts;
using Admission.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Aspose.Words;
using HtmlAgilityPack;


namespace Admission.Core.Services
{
    public class ArticleService:IArticlesService
    {
        private readonly IArticlesRepository _articlesRepository;
        private readonly IMediasRepository _mediasRepository;
        public ArticleService(IArticlesRepository articlesRepository, IMediasRepository mediasRepository)
        {
            _articlesRepository = articlesRepository;
            _mediasRepository = mediasRepository;
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


        public async Task ExtractNewsFromStream(Stream stream, string filepath)
        {
            // Lấy đường dẫn đến thư mục chứa Solution
            string solutionPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

            string outputDirectory = Path.Combine(solutionPath, "Admision.UI", "wwwroot", "StaticData");
            // Tải tài liệu Word
            Document doc = new Document(stream);
            string name = Path.GetFileNameWithoutExtension(filepath);
            string newFolderPath = Path.Combine(outputDirectory, name + " data");
            Directory.CreateDirectory(newFolderPath);
            // Chuyển đổi thành HTML
            string outputPath = Path.Combine(newFolderPath, name + ".html");
            doc.Save(outputPath, SaveFormat.Html);
            string htmlContent = System.IO.File.ReadAllText(outputPath);

            //xử lý html content
            Guid articleId = Guid.NewGuid();
            List<Media> mediaList = new List<Media>();
            string textContent = ParseHtmlContent(htmlContent, mediaList,articleId);

            // Lưu nội dung văn bản vào bảng new
            Article news = new Article { ArticleId = articleId,Content = textContent, Title = name, DateCreated = DateTime.Now };
            await  _articlesRepository.AddArticle(news);

            // Lưu media vào bảng media
            foreach (Media media in mediaList)
            {
                await _mediasRepository.AddMedia(media);  
            }
        }

        static string ParseHtmlContent(string html, List<Media> mediaList, Guid articleID)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            // Tìm tất cả các nút <img> và <video>
            var nodes = doc.DocumentNode.SelectNodes("//*"); // Lưu các nút vào danh sách để lặp lại
            // Tìm tất cả các nút <img> và <video>
            foreach (var node in nodes)
            {
                if (node.Name == "img")
                {
                    Guid id = Guid.NewGuid();
                    string mediaId = $"[media: {id}]"; // Tạo ID cho media
                    mediaList.Add(new Media {MediaID = id, MediaType = "image", MediaUrl = node.GetAttributeValue("src", ""),ArticleID= articleID});
                    var textNode = HtmlNode.CreateNode(mediaId);
                    node.ParentNode.ReplaceChild(textNode, node);
                }
                else if (node.Name == "video")
                {
                    Guid id = Guid.NewGuid();
                    string mediaId = $"[media: {id}]"; // Tạo ID cho media
                    mediaList.Add(new Media { MediaID = id, MediaType = "video/mp4", MediaUrl = node.GetAttributeValue("src", "") , ArticleID = articleID });
                    var textNode = HtmlNode.CreateNode(mediaId);
                    node.ParentNode.ReplaceChild(textNode, node);
                }
                
            }

            return doc.DocumentNode.InnerHtml.Trim();
        }

    }
}

