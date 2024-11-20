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
        private readonly IStudentsRepository _studentsRepository;
        private readonly IFilesService _filesService;

        public CommentsService(ICommentsRepository commentsRepository, IFilesService filesService, IStudentsRepository studentsRepository)
        {
            _commentsRepository = commentsRepository;
            _filesService = filesService;   
            _studentsRepository = studentsRepository;
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
            commentAdd.ParentCommentID = commentAddDTO.ParrentID != null ? commentAddDTO.ParrentID:null;
            commentAdd.level = commentAddDTO.level;
            CommentResponseDTO commentResponse = (await _commentsRepository.AddComment(commentAdd)).ToCommentResponseDTO();
            return commentResponse;
        }

        public async Task<List<CommentDTO>> GetCommentByPostId(Guid postID)
        {
            List<Comment> comments = await _commentsRepository.GetCommentsByPostId(postID);
            List<CommentDTO> results = comments.Select(c => c.ToCommentDTO()).ToList();
            foreach (CommentDTO result in results)
            {
                var student = (await _studentsRepository.GetAuthorByAuthorId(result.AuthorId));
                if(student != null)
                {
                    result.AuthorName = student.FirstName + student.LastName;
                }
                else result.AuthorName = "Anonymus";

            }
            return results;
        }
    }
}
