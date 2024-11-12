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
    public class PostsRepository : IPostsRepository
    {
        private readonly ApplicationDbContext _context;

        public PostsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PostMedia>> GetPostMediasByPostID(Guid postId)
        {
            return await _context.PostMedias
            .Where(pm => pm.PostID == postId)
            .ToListAsync();

        }

        public async Task<IEnumerable<Post>> GetPostsByGroupId(Guid groupId)
        {
            return await _context.Posts
            .Where(p => p.GroupID == groupId)
            .Include(p => p.Author)  
            .ToListAsync();
        }
    }
}
