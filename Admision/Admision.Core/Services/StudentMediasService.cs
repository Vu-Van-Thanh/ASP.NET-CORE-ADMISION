using Admission.Core.Domain.Entities;
using Admission.Core.Domain.RepositoryContracts;
using Admission.Core.DTO;
using Admission.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Services
{
    public class StudentMediasService : IStudentMediasService
    {
        private readonly IStudentMediasRepository _studentMediasRepository;

        public StudentMediasService(IStudentMediasRepository studentMediasRepository)
        {
            _studentMediasRepository = studentMediasRepository;
        }

        public async Task<List<StudentMediaDTO>> GetStudentMediasAsync(Guid StudentID)
        {
            List<StudentMedia>? list = await _studentMediasRepository.GetStudentMediaByStudentID(StudentID);
            List<StudentMediaDTO>? listDTO = list.Select(sm=>sm.ToSMDTO()).ToList();
            return listDTO;
        }
    }
}
