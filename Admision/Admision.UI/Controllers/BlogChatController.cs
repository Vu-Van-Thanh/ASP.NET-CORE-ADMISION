
using Admission.Core.DTO;
using Admission.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace Admission.UI.Controllers
{
    public class BlogChatController : Controller
    {
        private readonly IGroupsService _groupsService;
        private readonly IPostsService _postsService;

        public BlogChatController(IGroupsService groupsService,IPostsService postsService)
        {
            _groupsService = groupsService;
            _postsService = postsService;
        }

        public async Task<IActionResult> AllGroup()
        {
            List<GroupDTO> groups = (await _groupsService.GetAllGroups()).ToList();
            // lấy post mặc định của 1 nhóm
            List<PostContentDTO> posts = await _postsService.GetPostsByGroupId(Guid.Parse("e51c655a-25b5-4596-8ba8-1f711436febf"));
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
