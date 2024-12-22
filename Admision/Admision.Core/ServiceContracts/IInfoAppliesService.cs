using Admission.Core.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.ServiceContracts
{
    public interface IInfoAppliesService
    {
        Task<int> AddInfoApplies(InformationApplyDTO informationApplyDTO);
        Task<List<InfoStudentDTO>> GetInfoStudent(string ExamPeriod);
        Task<List<(string, string, int)>> ReadRoomsFromExcel(IFormFile roomsFile);
        Task<int> UpdateInfoApplies(List<InfoStudentDTO> informationStudentDTO);
    }
}
