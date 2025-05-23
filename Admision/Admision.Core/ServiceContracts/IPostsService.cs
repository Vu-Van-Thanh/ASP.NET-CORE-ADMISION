﻿using Admission.Core.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.ServiceContracts
{
    public interface IPostsService
    {
        Task<List<PostContentDTO>> GetPostsByGroupId(Guid  groupId);
        Task<List<PostMediaDTO>> GetPostMediasByPostId(Guid  postId);
        Task<PostDTO> AddPost(PostDTO post,string GroupID);
        Task<Guid> LikeMofify(string postId, int like);
    }
}
