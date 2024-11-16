
using Admission.Core.Domain.Entities;
using Admission.Core.DTO;
using Admission.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace Admission.UI.Controllers
{
    public class BlogChatController : Controller
    {
        private readonly IGroupsService _groupsService;
        private readonly IPostsService _postsService;
        private readonly IStudentsService _studentService;
        private readonly IStudentMediasService _studentMediaService;
        public BlogChatController(IGroupsService groupsService,IPostsService postsService, IStudentsService studentService, IStudentMediasService studentMediasService)
        {
            _groupsService = groupsService;
            _postsService = postsService;
            _studentService = studentService;
            _studentMediaService = studentMediasService;
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
            if(avatarUrl != null)
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
    }
}
