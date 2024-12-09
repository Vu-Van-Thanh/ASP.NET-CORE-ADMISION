using Admission.Core.DTO;
using Admission.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace Admission.UI.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentsService _commentsService;
        private readonly IFilesService _filesService;

        public CommentController(ICommentsService commentsService, IFilesService filesService)
        {
            _commentsService = commentsService;
            _filesService = filesService;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromForm] CommentAddClient commentAdd)
        {

            CommentAddDTO commentAddDTO = new CommentAddDTO();
            commentAddDTO.CommentText = commentAdd.commentText;
            commentAddDTO.CreatedDate = string.IsNullOrEmpty(commentAdd.CreatedDate) || !DateTime.TryParse(commentAdd.CreatedDate, out var createdDate) ? DateTime.Now : createdDate;
            commentAddDTO.ParrentID = string.IsNullOrEmpty(commentAdd.parrentCommentID) || !Guid.TryParse(commentAdd.parrentCommentID, out var temp) ? null : temp;
            commentAddDTO.PostID = commentAdd.postID; 
            commentAddDTO.UserID = commentAdd.AuthorID; 
            commentAddDTO.img = commentAdd.image;
            commentAddDTO.level = commentAdd.level;
            CommentResponseDTO result = await _commentsService.AddComment(commentAddDTO);
            var comment = new { commentID = result.CommentID.ToString(), Title = "Uploaded Comment" };
            return Ok(comment);
        }
        
    }
}
