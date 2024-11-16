using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.DTO
{
    public class CommentAddDTO
    {
        public Guid PostID { get; set; }
        public Guid UserID { get; set; }
        public Guid? ParrentID { get; set; }
        public string? CommentText { get; set; }
        public IFormFile? img { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
