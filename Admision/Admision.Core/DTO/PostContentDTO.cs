using Admission.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.DTO
{
    public class PostContentDTO
    {
        public Guid PostID { get; set; }
        public Guid AuthorID { get; set; }
        public string? Content { get; set; }
        public string AuthorName { get; set; }   
        public DateTime DateCreated { get; set; }
        public int LikeCount { get; set; }

    }
    public static class PostExtension
    {
        public static PostContentDTO ToPostContentDTO(this Post post)
        {
            return new PostContentDTO { AuthorID = post.AuthorID,PostID = post.PostID, Content = post.Content, AuthorName = post.Author.FirstName + " " + post.Author.LastName, DateCreated = post.DateCreated, LikeCount = post.LikeCount };
        }
    }
}
