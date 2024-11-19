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
    public class GroupService : IGroupsService
    {
        private readonly IGroupsRepository _groupRepository;
        private readonly IPostsRepository _postRepository;

        public GroupService(IGroupsRepository groupRepository, IPostsRepository postRepository)
        {
            _groupRepository = groupRepository;
            _postRepository = postRepository;
        }

        public async Task<IEnumerable<GroupDTO>> GetAllGroups()
        {
            List<Group> groups = (await _groupRepository.GetAllGroups()).ToList();
            return groups.Select(g => g.ToGroupDTO());

        }

        public async Task<GroupDTO> GetGroup(Guid groupId)
        {
            Group group = await _groupRepository.GetGroupById(groupId);
            return group.ToGroupDTO();
        }

        public async Task<IEnumerable<PostContentDTO>> GetPosts(Guid groupId)
        {
            List<Post> posts = (await _postRepository.GetPostsByGroupId(groupId)).ToList();
            return posts.Select(p => p.ToPostContentDTO());
        }
    }
}
