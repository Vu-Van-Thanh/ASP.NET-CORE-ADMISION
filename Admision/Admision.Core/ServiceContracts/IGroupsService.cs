using Admission.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.ServiceContracts
{
    public interface IGroupsService
    {
        Task<GroupDTO> GetGroup(Guid groupId);
        Task<IEnumerable<GroupDTO>> GetAllGroups();
        Task<IEnumerable<PostDTO>> GetPosts(Guid groupId);
    }
}
