using Admission.Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Domain.RepositoryContracts
{
    public interface ICommentsRepository
    {
        Task<List<Comment>> GetCommentsByPostId(Guid postId);
        Task<Comment> AddComment(Comment comment);
    }
}
