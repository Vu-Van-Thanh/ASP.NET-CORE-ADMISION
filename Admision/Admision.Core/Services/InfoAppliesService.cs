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
    public class InfoAppliesService : IInfoAppliesService
    {
        private readonly IInfoApplyRepository _infoApplyRepository;

        public InfoAppliesService(IInfoApplyRepository infoApplyRepository)
        {
            _infoApplyRepository = infoApplyRepository;
        }

        public async Task<int> AddInfoApplies(InformationApplyDTO informationApplyDTO)
        {
            InformationOfApplied informationOfApplied = new InformationOfApplied();
            informationOfApplied.Id = Guid.NewGuid();
            informationOfApplied.StudentID = informationApplyDTO.StudentID;
            informationOfApplied.AdmissionMethod = informationApplyDTO.AdmissionMethod;
            informationOfApplied.ExamPeriod = informationApplyDTO.ExamPeriod;
            informationOfApplied.MajorID = informationApplyDTO.MajorID;
            informationOfApplied.GPA11 = informationApplyDTO.GPA11;
            informationOfApplied.GPA10 = informationApplyDTO.GPA10;
            informationOfApplied.GPA12 = informationApplyDTO.GPA12;
            informationOfApplied.TestRoom = null;
            informationOfApplied.TestDate = DateTime.Now;

             return await _infoApplyRepository.AddInfoApply(informationOfApplied);
           

        }
    }
}
