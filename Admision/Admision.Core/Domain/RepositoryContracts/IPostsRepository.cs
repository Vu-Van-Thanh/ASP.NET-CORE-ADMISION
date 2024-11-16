using Admission.Core.Domain.Entities;
using Admission.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Domain.RepositoryContracts
{
    public interface IPostsRepository
    {
        Task<IEnumerable<Post>> GetPostsByGroupId(Guid groupId);
        Task<IEnumerable<PostMedia>> GetPostMediasByPostID(Guid postId);
        Task<Post> AddPostContent(PostContentDTO postContent,string GroupID);
        Task<PostMedia> AddPostMedia(PostMediaDTO postContent , Guid PostID);
    }
}
