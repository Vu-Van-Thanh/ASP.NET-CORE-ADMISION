using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.DTO
{
    public class CommentAddClient
    {
        public string? commentText { get; set; }
        public string? parrentCommentID { get; set; }
        public Guid postID { get; set; }
        public string CreatedDate { get; set; }
        public Guid AuthorID { get; set; }
        public IFormFile? image {  get; set; }
        public int level { get; set; }
    }
}
