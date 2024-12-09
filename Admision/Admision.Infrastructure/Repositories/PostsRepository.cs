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

        public async Task<Guid> AddLike(Guid postId, int like)
        {
            var post = await _context.Posts
                                      .FirstOrDefaultAsync(p => p.PostID == postId);  // Lấy bài viết theo postId

            if (post == null)
            {
                throw new Exception("Post not found");
            }

            // Thêm lượt thích vào bài viết, ví dụ:
            post.LikeCount += like;  // Giả sử bạn có thuộc tính Likes trong bài viết

            // Lưu thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();

            return post.PostID;  // Trả về Id của bài viết sau khi thêm lượt thích
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
