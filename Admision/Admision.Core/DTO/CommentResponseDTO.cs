using Admission.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.DTO
{
    public class CommentResponseDTO
    {
        public Guid CommentID { get; set; }
        public Guid PostID { get; set; }
        public Guid UserID { get; set; }
        public Guid? ParrentID { get; set; }
        public string? CommentText { get; set; }
        public string? imgUrl { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public static class CommentExtentsion
    {
        public static CommentResponseDTO ToCommentResponseDTO(this Comment comment)
        {
            return new CommentResponseDTO { CommentID = comment.CommentID,PostID=comment.PostID,UserID = comment.AuthorID,ParrentID=comment.ParentCommentID, CommentText= comment.Content,CreatedDate=comment.CreatedDate,imgUrl=comment.ImageUrl};
        }
    }

}
