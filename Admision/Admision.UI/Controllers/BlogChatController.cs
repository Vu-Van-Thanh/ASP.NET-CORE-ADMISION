
using Admission.Core.Domain.Entities;
using Admission.Core.DTO;
using Admission.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using NuGet.Packaging.Signing;

namespace Admission.UI.Controllers
{
    public class BlogChatController : Controller
    {
        private readonly IGroupsService _groupsService;
        private readonly IPostsService _postsService;
        private readonly ICommentsService _commentsService;
        private readonly IStudentsService _studentService;
        private readonly IStudentMediasService _studentMediaService;
        public BlogChatController(IGroupsService groupsService, IPostsService postsService, IStudentsService studentService, IStudentMediasService studentMediasService, ICommentsService commentsService)
        {
            _groupsService = groupsService;
            _postsService = postsService;
            _studentService = studentService;
            _studentMediaService = studentMediasService;
            _commentsService = commentsService;
        }

        public async Task<IActionResult> AllGroup(string GroupID = "e51c655a-25b5-4596-8ba8-1f711436febf")
        {
            // lấy thông tin người dùng 
            string Id = User.FindFirst("AccountID").Value;
            Student user = await _studentService.GetStudentByAccountID(Guid.Parse(Id));
            ViewBag.Author = user.FirstName + user.LastName;
            ViewBag.AuthorID = user.StudentID;
            List<StudentMediaDTO> mediaList = await _studentMediaService.GetStudentMediasAsync(user.StudentID);
            string? avatarUrl = mediaList.FirstOrDefault(media => media.StudentMediaType == "AvartaPath")?.MediaUrl;
            if (avatarUrl != null)
            {
                ViewBag.AvatarUrl = avatarUrl;
            }
            else
            {
                ViewBag.AvatarUrl = "/img/default_avatar.jpg";
            }

            List<GroupDTO> groups = (await _groupsService.GetAllGroups()).ToList();
            // lấy post mặc định của 1 nhóm
            List<PostContentDTO> posts = await _postsService.GetPostsByGroupId(Guid.Parse(GroupID));
            List<PostDTO> postDTOs = new List<PostDTO>();
            foreach (PostContentDTO post in posts)
            {
                PostDTO postDTO = new PostDTO();
                List<PostMediaDTO> medias = await _postsService.GetPostMediasByPostId(post.PostID);
                postDTO.media = medias;
                postDTO.content = post;
                postDTOs.Add(postDTO);
            }
            BlogChatDTO blogChatDTO = new BlogChatDTO();
            blogChatDTO.groups = groups;
            blogChatDTO.posts = postDTOs;
            return View(blogChatDTO);
        }

        public async Task<IActionResult> GetPostByGroup(string groupID, int page = 1, int pageSize = 10)
        {
            List<PostContentDTO> posts = await _postsService.GetPostsByGroupId(Guid.Parse(groupID));
            List<PostContentDTO> paginatedPosts = posts
            .Skip((page - 1) * pageSize) 
            .Take(pageSize)            
            .ToList();
            List<PostDTO> postDTOs = new List<PostDTO>();
            foreach (PostContentDTO post in paginatedPosts)
            {
                PostDTO postDTO = new PostDTO();
                List<PostMediaDTO> medias = await _postsService.GetPostMediasByPostId(post.PostID);
                postDTO.media = medias;
                postDTO.content = post;
                postDTOs.Add(postDTO);
            }
            List<PostData> data = new List<PostData>();
            foreach (PostDTO post in postDTOs)
            {
                PostData temp = new PostData();
                temp.author = post.content.AuthorName;
                temp.postId = post.content.PostID.ToString();
                temp.content = post.content.Content;
                temp.timestamp = post.content.DateCreated.ToString();
                temp.likes = post.content.LikeCount;

                temp.comments = (await _commentsService.GetCommentByPostId(post.content.PostID)).Count;
                foreach (PostMediaDTO postMediaDTO in post.media)
                {
                    MediaData mediaData = new MediaData();
                    mediaData.type = postMediaDTO.Type;
                    mediaData.src = postMediaDTO.MediaUrl;
                    temp.mediaItems.Add(mediaData);
                }
                data.Add(temp);

            }

            var dataList = data.Select(post => new
            {
                postId = post.postId,
                author = post.author,
                timestamp = post.timestamp,
                content = post.content,
                likes = post.likes,
                comments = post.comments,
                mediaItems = post.mediaItems.Select(media => new
                {
                    type = media.type,
                    src = media.src
                }).ToList() 
            }).ToList();

            return Ok(dataList);
        }


        public async Task<IActionResult> GetCommentByPost(string postId, int page = 1, int pageSize = 10)
        {
            List<CommentDTO> comments = await _commentsService.GetCommentByPostId(Guid.Parse(postId));
            List<CommentDTO> paginatedComments = comments
           .Skip((page - 1) * pageSize)
           .Take(pageSize)
           .ToList();
            foreach (CommentDTO commentItem in paginatedComments)
            {
                List<StudentMediaDTO> media = await _studentMediaService.GetStudentMediasAsync(commentItem.AuthorId);
                StudentMediaDTO?  avatarMedia = media.FirstOrDefault(md => md.StudentMediaType == "avatar");
                if (avatarMedia != null)
                {
                    commentItem.AuthorAvatar = avatarMedia.MediaUrl;
                    
                }
                else
                {
                    commentItem.AuthorAvatar = "/img/default_avatar.jpg";
                }
            }
            var dataList = paginatedComments.Select(comment => new
            {
                id = comment.CommentId.ToString(),
                imgurl = comment.ImageUrl != null ? comment.ImageUrl : null,
                author = comment.AuthorName,
                time = comment.DateCreated.ToString(),
                text = comment.Content != null ? comment.Content : "",
                avatar = comment.AuthorAvatar,
                level = comment.level,
                parentID = comment.ParrentCommentId != null ? comment.ParrentCommentId.ToString() : null
            }).ToList();
            return Ok(dataList);
        }

    }
}
