using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admission.Core.Domain.Entities
{
    public class Comment
    {
        [Key]
        public Guid CommentID { get; set; }

        public Guid PostID { get; set; }
        public Guid AuthorID { get; set; }

        public string? Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LikeCount { get; set; }
        public int level {  get; set; }

        // URL cho ảnh và video
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }

        [ForeignKey("PostID")]
        public virtual Post? Post { get; set; }

        [ForeignKey("AuthorID")]
        public virtual Student? Student { get; set; }

        public Guid? ParentCommentID { get; set; }

        [ForeignKey("ParentCommentID")]
        public virtual Comment? ParentComment { get; set; }

        public virtual ICollection<Comment>? Replies { get; set; }
    }
}
