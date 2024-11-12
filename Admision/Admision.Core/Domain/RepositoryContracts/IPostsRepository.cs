using Admission.Core.Domain.Entities;
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
    }
}
