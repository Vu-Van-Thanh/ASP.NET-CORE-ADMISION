using Admission.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.DTO
{
    public class PostDTO
    {
        public Guid PostID { get; set; }
        public string Content { get; set; }
        public string AuthorName { get; set; }   
        public DateTime DateCreated { get; set; }
        public int LikeCount { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
    }
    public static class PostExtension
    {
        public static PostDTO ToPostDTO(this Post post)
        {
            return new PostDTO { PostID = post.PostID, Content = post.Content, AuthorName = post.Author.FirstName + " " + post.Author.LastName, DateCreated = post.DateCreated, LikeCount = post.LikeCount };
        }
    }
}
