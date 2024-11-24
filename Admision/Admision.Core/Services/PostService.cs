using Admission.Core.Domain.Entities;
using Admission.Core.Domain.RepositoryContracts;
using Admission.Core.DTO;
using Admission.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Services
{
    public class PostService : IPostsService
    {
        private readonly IPostsRepository _postsRepository;

        public PostService(IPostsRepository postsRepository)
        {
            _postsRepository = postsRepository;
        }

        public async Task<PostDTO> AddPost(PostDTO post, string GroupID)
        {
            await _postsRepository.AddPostContent(post.content,GroupID);
            foreach (PostMediaDTO media in post.media)
            {
                await _postsRepository.AddPostMedia(media, post.content.PostID);
            }
            return post;
        }

        public async  Task<List<PostMediaDTO>> GetPostMediasByPostId(Guid postId)
        {
            List<PostMedia> posts = (await _postsRepository.GetPostMediasByPostID(postId)).ToList();
            List<PostMediaDTO> postMediaDTOs = posts.Select(post=>post.ToPostMediaDTO()).ToList();
            return postMediaDTOs;
        }

        public async Task<List<PostContentDTO>> GetPostsByGroupId(Guid groupId)
        {
            List<Post> posts = (await _postsRepository.GetPostsByGroupId(groupId)).ToList();
            List<PostContentDTO> postDTOs = posts.Select(post =>  post.ToPostContentDTO()).ToList();
            return postDTOs;
        }

        public async Task<Guid> LikeMofify(string postId, int like)
        {
            Guid result = await _postsRepository.AddLike(Guid.Parse(postId), like);
            return result;
        }
    }
}
