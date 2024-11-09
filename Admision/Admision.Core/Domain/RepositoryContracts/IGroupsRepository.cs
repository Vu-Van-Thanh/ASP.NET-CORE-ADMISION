using Admission.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Domain.RepositoryContracts
{
    public interface IGroupsRepository
    {
        Task<Group> GetGroupById(Guid groupId);
        Task<IEnumerable<Group>> GetAllGroups();
        Task<IEnumerable<Post>> GetAllPostsInGroup(Guid groupId); // Lấy tất cả bài viết trong group
    }
}
