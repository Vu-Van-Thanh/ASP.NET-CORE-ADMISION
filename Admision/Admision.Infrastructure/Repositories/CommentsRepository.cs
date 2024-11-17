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
            Console.WriteLine($"Before AddAsync: {JsonConvert.SerializeObject(comment)}");
            await _context.Comments.AddAsync(comment);
            Console.WriteLine($"After AddAsync: {JsonConvert.SerializeObject(comment)}");
            await _context.SaveChangesAsync();
            Console.WriteLine($"After AddAsync: {JsonConvert.SerializeObject(comment)}");

            return comment;
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostId(Guid postId)
        {
            return await _context.Comments
            .Where(c => c.PostID == postId)
            .Include(c => c.Student)
            .ToListAsync();
        }
    }
}
