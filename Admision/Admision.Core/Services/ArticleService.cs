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
            // Lấy đường dẫn thư mục hiện tại của project Admision.Core
            string coreProjectPath = Directory.GetCurrentDirectory();

            // Lấy thời gian hiện tại
            DateTime currentTime = DateTime.Now;

            // Lấy năm và tên tháng bằng tiếng Anh
            string year = currentTime.Year.ToString();         // Lấy năm
            string month = currentTime.ToString("MMMM");       // Lấy tên tháng (January, February,...)

            // Kết hợp đường dẫn với wwwroot/StaticData/năm/tháng
            string outputDirectory = Path.Combine(coreProjectPath, "wwwroot", "StaticData", year, month);

            // Kiểm tra xem thư mục đã tồn tại chưa, nếu chưa thì tạo
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
            string folderUrl = $"/StaticData/{year}/{monthName}/{nameFolder}{articleID}/"; // Đường dẫn tương đối từ gốc của ứng dụng

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
                    string mediaId = $"[media: {id}]"; // Tạo ID cho media

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




    }
}

