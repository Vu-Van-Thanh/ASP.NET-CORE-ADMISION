using Admission.Core.Domain.Entities;
using Admission.Core.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.ServiceContracts
{
	public interface IStudentsService
	{
		Task<Student?> GetStudentByAccountID (Guid accountID);
		Task<Student?> GetStudentByStudentID (Guid studentID);

        Task<StudentInfoDTO?> GetUserInfoAsync(string userId);
		Task<Guid> UpdateStudentInfo (StudentUpdateInfoDTO student, Guid accountId);
		Task<Guid> UpdateStudyInfo(StudyProcessDTO student, string accountId);
		Task SaveMediaStudent(IFormFile[] formFiles, Guid Id, string type);
		Task AddAdditionalInformation(Student student);
		(string, string) SplitStringAtFirstSpace(string input);
        Task<bool> UpdateRegisEx(Student updateStudent);
        Task<bool> UpdateRegisMediaEx(StudentMedia updateStudent);
		Task<bool> UpdateStudentMedia(List<StudentMedia> list);



    }
}
