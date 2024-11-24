using Admission.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Domain.RepositoryContracts
{
    public interface IStudentMediasRepository
    {
        Task<List<StudentMedia>?> GetStudentMediaByStudentID (Guid studentID);
        Task UpdateStudentMedia(List<StudentMedia> medias);
    }
}
