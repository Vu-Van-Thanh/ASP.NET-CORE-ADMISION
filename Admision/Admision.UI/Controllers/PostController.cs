using Admission.Core.Domain.Entities;
using Admission.Core.DTO;
using Admission.Core.ServiceContracts;
using Admission.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Admission.UI.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostsService _postsService;
        private readonly IFilesService _filesService;

        public PostController(IPostsService postsService, IFilesService filesService)
        {
            _postsService = postsService;
            _filesService = filesService;
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(string AuthorID,string AuthorName,string GroupID,string UserID, string PostText, string DateCreated, IFormFile[] mediaFiles)
        {
            Guid postID = Guid.NewGuid();
            // tạo file
            string targetPath = $"PostMedia/{postID}";
            List<string> savedFilePaths = await _filesService.SaveMediaFilesAsync(mediaFiles, targetPath);

            // Content
            PostContentDTO postContentDTO = new PostContentDTO();
            postContentDTO.PostID = postID;
            postContentDTO.Content = PostText;
            postContentDTO.AuthorID = Guid.Parse(AuthorID);
            postContentDTO.AuthorName = AuthorName;
            postContentDTO.DateCreated = DateTime.Parse(DateCreated);
            postContentDTO.LikeCount = 0;
            // Media
            List<PostMediaDTO> postMediaDTO = new List<PostMediaDTO>();
            foreach(string file in savedFilePaths)
            {
                PostMediaDTO postMedia = new PostMediaDTO();
                postMedia.MediaID = Guid.NewGuid();
                postMedia.MediaUrl = file;
                postMedia.Type = "PostMedia";
                postMediaDTO.Add(postMedia);
            }
            PostDTO postDTO = new PostDTO();
            postDTO.content = postContentDTO;
            postDTO.media = postMediaDTO;


            await _postsService.AddPost(postDTO,GroupID);

            var postData = new { postID = postID,Title = "Post Title", Content = "This is the content of the post." };
            return Ok(postData);  // Trả về HTTP 200 OK và dữ liệu JSON
        }
    }
}
