using Admission.Core.Domain.Entities;
using Admission.Core.Domain.RepositoryContracts;
using Admission.Core.DTO;
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

        public async Task<Post> AddPostContent(PostContentDTO postContent, string GroupID)
        {
            Post post = new Post();
            post.PostID = postContent.PostID;
            post.AuthorID = postContent.AuthorID;
            post.Content = postContent.Content;
            post.DateCreated = postContent.DateCreated;
            post.GroupID = Guid.Parse(GroupID);
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            return post;   
        }

        public async Task<PostMedia> AddPostMedia(PostMediaDTO postContent,Guid PostID)
        {
            PostMedia postMedia = new PostMedia();
            postMedia.PostID = PostID;
            postMedia.MediaID = postContent.MediaID;
            postMedia.MediaUrl = postContent.MediaUrl;
            postMedia.Type = postContent.Type;
            await _context.PostMedias.AddAsync(postMedia);
            await _context.SaveChangesAsync();
            return postMedia;
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
