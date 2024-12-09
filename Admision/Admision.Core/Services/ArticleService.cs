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
    public class ArticleService : IArticlesService
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
            return new Article() { Title = "Tin tức mặc định", Content = "Không có thông tin", DateCreated = DateTime.Now };
        }

        public async Task<string> RenderArticleContent(Article article)
        {
            string? content = article.Content;
            if (content != null)
            {
                foreach (Media media in article.medias)
                {
                    content = content.Replace($"[media: {media.MediaID}]", media.MediaUrl);
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
            Guid articleId = Guid.NewGuid();
            DateTime datePost = DateTime.Now;
            // Lấy đường dẫn đến thư mục chứa Solution
            string outputDirectory = GetStaticDataPathWithDate();
            // Tải tài liệu Word
            Document doc = new Document(stream);
            string name = Path.GetFileNameWithoutExtension(filepath);

            string newFolderPath = Path.Combine(outputDirectory, name + " data " + articleId.ToString());
            Directory.CreateDirectory(newFolderPath);
            // Chuyển đổi thành HTML
            string outputPath = Path.Combine(newFolderPath, name + ".html");
            doc.Save(outputPath, SaveFormat.Html);
            string htmlContent = System.IO.File.ReadAllText(outputPath);
            //xử lý html content
            List<Media> mediaList = new List<Media>();
            string textContent = ParseHtmlContent(htmlContent, mediaList, articleId, datePost, name + " data ");

            // Lưu nội dung văn bản vào bảng new
            Article news = new Article { ArticleId = articleId, Content = textContent, Title = name, DateCreated = datePost };
            await _articlesRepository.AddArticle(news);

            // Lưu media vào bảng media
            foreach (Media media in mediaList)
            {
                await _mediasRepository.AddMedia(media);
            }
        }

        public string GetStaticDataPathWithDate()
        {
            
            string coreProjectPath = Directory.GetCurrentDirectory();

            
            DateTime currentTime = DateTime.Now;

            // Lấy năm và tên tháng bằng tiếng Anh
            string year = currentTime.Year.ToString();         
            string month = currentTime.ToString("MMMM");       

            
            string outputDirectory = Path.Combine(coreProjectPath, "wwwroot", "StaticData", year, month);

            
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            return outputDirectory;
        }



        static string ParseHtmlContent(string html, List<Media> mediaList, Guid articleID, DateTime datepost, string nameFolder)
        {
            // Chuyển đổi tháng thành chữ
            string monthName = datepost.ToString("MMMM", new System.Globalization.CultureInfo("en-US"));
            string year = datepost.Year.ToString();

            // Tạo đường dẫn URL media tương đối
            string folderUrl = $"/StaticData/{year}/{monthName}/{nameFolder}{articleID}/";

            // Đường dẫn thư mục vật lý (hệ thống tệp) để lưu trữ media
            string rootFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "StaticData", year, monthName);
            string targetFolder = Path.Combine(rootFolder, $"{nameFolder}{articleID}");

            // Tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(targetFolder))
            {
                Directory.CreateDirectory(targetFolder); // Tạo thư mục mới
            }

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            // Tìm tất cả các nút <img> và <video>
            var nodes = doc.DocumentNode.SelectNodes("//*");

            foreach (var node in nodes)
            {
                if (node.Name == "img")
                {
                    Guid id = Guid.NewGuid();
                    string mediaId = $"[media: {id}]";

                    // Tạo tên tệp cho hình ảnh dựa trên ID của media
                    string mediaFileName = node.GetAttributeValue("src", "");

                    // Tạo URL media (tương đối)
                    string mediaUrl = $"{folderUrl}{mediaFileName}";
                    mediaList.Add(new Media { MediaID = id, MediaType = "image", MediaUrl = mediaUrl, ArticleID = articleID });

                    // Thay thế thuộc tính src của thẻ img bằng ID media
                    node.SetAttributeValue("src", mediaId);
                }
                else if (node.Name == "video")
                {
                    Guid id = Guid.NewGuid();
                    string mediaId = $"[media: {id}]"; // Tạo ID cho media

                    // Tạo tên tệp cho video dựa trên ID của media
                    string mediaFileName = $"video_{id}.mp4";

                    // Tạo URL media (tương đối)
                    string mediaUrl = $"{folderUrl}{mediaFileName}";
                    mediaList.Add(new Media { MediaID = id, MediaType = "video/mp4", MediaUrl = mediaUrl, ArticleID = articleID });

                    // Thay thế thuộc tính src của thẻ video bằng ID media
                    node.SetAttributeValue("src", mediaId);
                }
            }

            // Xóa các thẻ <p> chứa "policy" của Aspose
            var paragraphs = doc.DocumentNode.SelectNodes("//p");
            if (paragraphs != null)
            {
                foreach (var p in paragraphs)
                {
                    if (p.InnerText.Contains("Aspose"))
                    {
                        p.Remove(); // Xóa thẻ <p> chứa từ khóa "Aspose"
                    }
                }
            }

            return doc.DocumentNode.InnerHtml.Trim();
        }



        public async Task AddOrUpdateArticlesFromFiles(String docxFiles, string year, string majorCode)
        {
            
                try
                {
                    
                    string articleType = $"{year} {majorCode}";

                    
                    var existingArticle = await _articlesRepository.GetArticleByType(articleType);

                    // Đọc nội dung file Word
                    using var fileStream = new FileStream(docxFiles, FileMode.Open, FileAccess.Read);
                    Document doc = new Document(fileStream);

                    // Tạo đường dẫn để lưu tạm HTML (dùng cho phân tích nội dung)
                    Guid articleId = existingArticle?.ArticleId ?? Guid.NewGuid();
                    string outputDirectory = GetStaticDataPathWithDate();
                    string tempFolderPath = Path.Combine(outputDirectory, majorCode + " data " + articleId);
                    Directory.CreateDirectory(tempFolderPath);

                    string fileName = Path.GetFileNameWithoutExtension(docxFiles);
                    string htmlPath = Path.Combine(tempFolderPath, fileName + ".html");
                    doc.Save(htmlPath, SaveFormat.Html);

                    // Đọc nội dung HTML
                    string htmlContent = System.IO.File.ReadAllText(htmlPath);

                    // Phân tích nội dung HTML và lấy text + media
                    List<Media> mediaList = new List<Media>();
                    string textContent = ParseHtmlContent(htmlContent, mediaList, articleId, DateTime.Now, majorCode + " data ");

                    // Nếu bài viết đã tồn tại, cập nhật nội dung
                    if (existingArticle != null)
                    {
                        existingArticle.Content = textContent;
                        existingArticle.DateCreated = DateTime.Now;
                        await _articlesRepository.UpdateArticle(existingArticle);
                        Console.WriteLine($"Đã cập nhật bài viết: {existingArticle.ArticleId} - {articleType}");
                    }
                    else
                    {
                        // Nếu bài viết chưa tồn tại, thêm mới
                        Article newArticle = new Article
                        {
                            ArticleId = articleId,
                            Title = fileName,
                            Content = textContent,
                            DateCreated = DateTime.Now,
                            Type  = articleType
                        };
                        await _articlesRepository.AddArticle(newArticle);
                        Console.WriteLine($"Đã thêm bài viết mới: {newArticle.ArticleId} - {articleType}");
                    }

                    // Lưu media vào bảng Media
                    foreach (var media in mediaList)
                    {
                        await _mediasRepository.AddMedia(media);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi khi xử lý file {docxFiles}: {ex.Message}");
                }
            
        }

        
        public async Task<Article?> GetArticleByType(string type)
        {
            return await _articlesRepository.GetArticleByType(type);
        }

    }
}

