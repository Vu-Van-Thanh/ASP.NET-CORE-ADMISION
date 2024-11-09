using Admission.Core.Domain.Entities;
using Admission.Core.Domain.RepositoryContracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Infrastructure.Repositories
{
    public class GroupsRepository : IGroupsRepository
    {
        private readonly ApplicationDbContext _context;

        public GroupsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Group>> GetAllGroups()
        {
            return await _context.Groups.ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetAllPostsInGroup(Guid groupId)
        {
            return await _context.Posts
                         .Where(p => p.GroupID == groupId)
                         .Include(p=>p.Author)
                         .ToListAsync();
        }

        public async Task<Group> GetGroupById(Guid groupId)
        {
            return await _context.Groups
            .Include(g => g.Posts)  // Bao gồm bài viết nếu cần lấy group với các bài viết
            .FirstOrDefaultAsync(g => g.GroupID == groupId);
        }
    }
}
