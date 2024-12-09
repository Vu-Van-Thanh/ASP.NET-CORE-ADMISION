using Admission.Core.Domain.Entities;
using Admission.Core.Domain.RepositoryContracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Infrastructure.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> AddComment(Comment comment)
        {         
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }


        public async Task<List<Comment>> GetCommentsByPostId(Guid postId)
        {
            return await _context.Comments
            .Where(c => c.PostID == postId)
            .Include(c => c.Student)
            .ToListAsync();
        }
    }
}
