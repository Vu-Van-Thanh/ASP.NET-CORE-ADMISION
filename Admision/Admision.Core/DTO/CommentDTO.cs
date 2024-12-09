using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.DTO
{
    public class CommentDTO
    {
        public Guid CommentId { get; set; }
        public string? Content { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorAvatar { get; set; } = "/img/default_avatar.jpg";
        public Guid PostId { get; set; }
        public Guid? ParrentCommentId { get; set; }
        public string AuthorName { get; set; }
        public DateTime DateCreated { get; set; }

        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public int level { get; set; }
        public int likeCount { get; set; }
    }

    
}
