using Admission.Core.Domain.Entities;
using Admission.Core.Domain.RepositoryContracts;
using Admission.Core.DTO;
using Admission.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly IFilesService _filesService;

        public CommentsService(ICommentsRepository commentsRepository, IFilesService filesService)
        {
            _commentsRepository = commentsRepository;
            _filesService = filesService;   
        }

        public async Task<CommentResponseDTO> AddComment(CommentAddDTO commentAddDTO)
        {
            Guid commentID = Guid.NewGuid();
            Comment commentAdd = new Comment();
            IFormFile[] imgList = new IFormFile[] { commentAddDTO.img };
            List<string> savedFile = new List<string>();
            if (imgList == null || imgList.Length == 0 || imgList[0] == null)
            {
                commentAdd.ImageUrl = null;
            }
            else
            {
                savedFile = await _filesService.SaveMediaFilesAsync(imgList,$"CommentMedia/{commentID}");
                commentAdd.ImageUrl = savedFile[0];
            }
            commentAdd.AuthorID = commentAddDTO.UserID;
            commentAdd.CommentID = commentID;
            commentAdd.PostID = commentAddDTO.PostID;
            commentAdd.CreatedDate = commentAddDTO.CreatedDate;
            commentAdd.LikeCount = 0;
            commentAdd.VideoUrl = null;
            commentAdd.Content = commentAddDTO.CommentText;
            commentAdd.ParentCommentID = null;
            CommentResponseDTO commentResponse = (await _commentsRepository.AddComment(commentAdd)).ToCommentResponseDTO();
            return commentResponse;
        }
    }
}
