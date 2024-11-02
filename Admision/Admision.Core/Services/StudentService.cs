using Admission.Core.Domain.Entities;
using Admission.Core.Domain.IdentityEntities;
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
	public class StudentService : IStudentsService
	{
		private readonly IStudentsRepository _studentsRepository;

		
		public StudentService(IStudentsRepository studentsRepository)
		{
			_studentsRepository = studentsRepository;
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
				studentInfo.PathOfAvatar = student.PathOfAvatar;
				studentInfo.HighSchoolID = student.HighSchoolID;
				studentInfo.Address = student.Address;
				studentInfo.Email = user.Email;
				studentInfo.Phone = user.PhoneNumber;
				return studentInfo;

			}
		}

		public async Task UpdateStudentInfoAsync(StudentInfoDTO studentInfoDTO)
		{
			ApplicationUser? user = await _studentsRepository.GetUserByEmailAsync(studentInfoDTO.Email);
			if (user == null) return;

			Student? student = await _studentsRepository.GetStudentByAccountID(user.Id);
			student.FirstName = studentInfoDTO.FirstName;
			student.LastName = studentInfoDTO.LastName;
			student.DateOfBirth = studentInfoDTO.DateOfBirth;
			student.Gender = studentInfoDTO.Gender;
			student.HighSchoolID = studentInfoDTO.HighSchoolID;
			student.Address = studentInfoDTO.Address;
			student.PathOfAvatar = studentInfoDTO.PathOfAvatar;
			user.PhoneNumber = studentInfoDTO.Phone;

			await _studentsRepository.UpdateUserAsync(user,student);
		}
	}
}
