using Admission.Core.Domain.Entities;
using Admission.Core.Domain.IdentityEntities;
using Admission.Core.Domain.RepositoryContracts;
using Admission.Core.DTO;
using Admission.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Services
{
	public class StudentService : IStudentsService
	{
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStudentsRepository _studentsRepository;
		private readonly IStudentMediasRepository _studentMediasRepository;
		private readonly IFilesService _filesService;

		
		public StudentService(IStudentsRepository studentsRepository, IFilesService filesService, UserManager<ApplicationUser> userManager, IStudentMediasRepository studentMediasRepository)
        {
            _studentsRepository = studentsRepository;
            _filesService = filesService;
            _userManager = userManager;
            _studentMediasRepository = studentMediasRepository;
        }
        public async Task<Student?> GetStudentByAccountID(Guid accountID)
		{
			Student? student = await _studentsRepository.GetStudentByAccountID(accountID);
			return student;
		}

		public async Task<StudentInfoDTO?> GetUserInfoAsync(string userId)
		{
			StudentInfoDTO studentInfo = new StudentInfoDTO();
			Student? student = await _studentsRepository.GetStudentByAccountID(Guid.Parse(userId));
			ApplicationUser? user = await _studentsRepository.GetUserAsync(userId);
			if (student == null)
			{
				return null;
			}
			else
			{
				studentInfo.FirstName = student.FirstName;
				studentInfo.LastName = student.LastName;
				studentInfo.DateOfBirth = student.DateOfBirth;
				studentInfo.Gender = student.Gender;
				studentInfo.PathOfAvatar = "default_avatar.jpg";
				studentInfo.HighSchoolID = student.HighSchoolID;
				studentInfo.Address = student.Address;
				studentInfo.Email = user.Email;
				studentInfo.Phone = user.PhoneNumber;
				return studentInfo;
			}
		}

        public async Task<Guid> UpdateStudentInfo(StudentUpdateInfoDTO student, Guid accountId)
        {
            Student? user = await  _studentsRepository.GetStudentByAccountID(accountId);
            if (Enum.TryParse<GenderOptions>(student.Gender, true, out var gender))
            {
                user.Gender = gender;
            }
            else
            {
                
                user.Gender = GenderOptions.Male;
            }
			user.Nationality = student.Nationality != null ? student.Nationality : "Việt Nam";
			user.Ethnic = student.Ethnic != null ? student.Ethnic : "Kinh";
			user.Religion = student.Religion != null ? student.Religion : "Không có";
			user.PlaceOfBirth = student.PlaceOfBirth;
			user.DateOfBirth = student.DateOfBirth;
			user.IndentityCard = student.IndentityCard;
			user.PlaceIssued = student.PlaceIssued;
            if (Guid.TryParse(student.Country, out var countryId))
            {
                user.CountryID = countryId;
            }
            else
            {
                user.CountryID = Guid.Parse("4AC574FB-85A9-4159-9F2B-053B2BC6FC7E");
            }

            

            user.Province = student.Province;
			user.District = student.District;
			user.Commune = student.Commune;
			user.Address = student.Address;
			if(student.RelativeName != null)
			{
				user.ContactRelative = new Relative();
                user.ContactRelative.StudentID = user.StudentID;
                user.ContactRelative.RelativeType = student.RelativePos ?? "Không rõ";
                user.ContactRelative.RelativeName = student.RelativeName;
                user.ContactRelative.Phone = student.Phone;
            }
            
			
			user.CandidateType = student.CandidateType;
			user.Member = student.Member;
            user.JoiningDate = student.JoiningDate ?? DateTime.Parse("2024-11-21T14:30:00");
			user.PlaceJoining = student.PlaceJoining;
			user.MembershipBook = student.MembershipBook;
			user.MembershipCard = student.MembershipCard;
			user.Partisan = student.Partisan;
			user.HealthStatus = student.HealthStatus;
			user.InsuranceNumber = student.InsuranceNumber;
			List<string> BHYTLink = await _filesService.SaveMediaFilesAsync(student.BHYTImage, $"BHYTImage/{user.StudentID}");
			List<string> CCCDLink = await _filesService.SaveMediaFilesAsync(student.CCCDImage, $"CCCDImage/{user.StudentID}");
			List<StudentMedia> BHYT = new List<StudentMedia>();
			List<StudentMedia> CCCD = new List<StudentMedia>();
            foreach (string media in BHYTLink)
			{
				StudentMedia temp = new StudentMedia();
				temp.StudentMediaID = Guid.NewGuid();
				temp.StudentID = user.StudentID;
				temp.MediaUrl = media;
				temp.StudentMediaType = "BHYT";
				BHYT.Add(temp);
			}
            foreach (string media in CCCDLink)
            {
                StudentMedia temp = new StudentMedia();
                temp.StudentMediaID = Guid.NewGuid();
                temp.StudentID = user.StudentID;
                temp.MediaUrl = media;
                temp.StudentMediaType = "CCCD";
                BHYT.Add(temp);
            }
			await _studentMediasRepository.UpdateStudentMedia(BHYT);
			await _studentMediasRepository.UpdateStudentMedia(CCCD);
			var account = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == accountId);
			account.PhoneNumber = student.Phone;
			account.Email = student.Email;
            await _userManager.UpdateAsync(account);
			await _studentsRepository.UpdateStudent(user);
			return user.StudentID;
        }


		public async Task<Guid> UpdateStudyInfo(StudyProcessDTO student, string accountId)
		{
			Student existStudent = await _studentsRepository.GetStudentByAccountID(Guid.Parse(accountId));
			existStudent.AcademicPerformance10 = student.AcademicPerformance10;
			existStudent.AcademicPerformance11 = student.AcademicPerformance11;
			existStudent.AcademicPerformance12 = student.AcademicPerformance12;
			existStudent.Conduct10 = student.Conduct10;
			existStudent.Conduct11 = student.Conduct11;
			existStudent.Conduct12 = student.Conduct12;
			existStudent.OutstandingAchievements = student.OutstandingAchievements;
            List<StudentMedia> HB = new List<StudentMedia>();
            List<string> HBLink = await _filesService.SaveMediaFilesAsync(student.HBImage, $"HBImage/{existStudent.StudentID}");
            foreach (string media in HBLink)
            {
                StudentMedia temp = new StudentMedia();
                temp.StudentMediaID = Guid.NewGuid();
                temp.StudentID = existStudent.StudentID;
                temp.MediaUrl = media;
                temp.StudentMediaType = "HB";
                HB.Add(temp);
            }
            await _studentMediasRepository.UpdateStudentMedia(HB);
			await _studentsRepository.UpdateStudent(existStudent);
			return existStudent.StudentID;
		}

		public async Task SaveMediaStudent(IFormFile[] formFiles, Guid Id, string type)
		{
            List<string>? savedFile = await _filesService.SaveMediaFilesAsync(formFiles, $"{type}/{Id}");
            List<StudentMedia> medias = new List<StudentMedia>();
            foreach (string media in savedFile)
            {
                StudentMedia temp = new StudentMedia();
                temp.StudentMediaID = Guid.NewGuid();
                temp.StudentID = Id;
                temp.MediaUrl = media;
                temp.StudentMediaType = type;
                medias.Add(temp);
            }
            await _studentMediasRepository.UpdateStudentMedia(medias);

        }

        public async Task AddAdditionalInformation(Student student)
        {
            await _studentsRepository.UpdateStudent(student);
        }
    }
}
