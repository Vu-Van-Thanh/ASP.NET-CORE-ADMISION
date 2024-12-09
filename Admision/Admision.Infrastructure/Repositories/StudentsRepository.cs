using Admission.Core.Domain.Entities;
using Admission.Core.Domain.IdentityEntities;
using Admission.Core.Domain.RepositoryContracts;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Infrastructure.Repositories
{
    public class StudentsRepository : IStudentsRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly UserManager<ApplicationUser> _userManager;
        public StudentsRepository(ApplicationDbContext dbcontext, UserManager<ApplicationUser> userManager)
        {
            _dbcontext = dbcontext;
            _userManager = userManager;
        }

        public async Task<ApplicationUser?> GetUserAsync(string accountID)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(accountID);
            if (user == null)
            {
                return null;
            }
            return user;


        }

        public async Task<Student?> GetStudentByAccountID(Guid accountID)
        {
            return await _dbcontext.Students
                        .Include(s => s.ApplicationUser)
                        .FirstOrDefaultAsync(s => s.ApplicationUser.Id == accountID);
        }

        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);
            return user;
        }

        public async Task UpdateUserAsync(ApplicationUser user, Student student)
        {
            _dbcontext.Update(student);
            await _userManager.UpdateAsync(user);

            await _dbcontext.SaveChangesAsync();

        }

        public async Task UpdateStudent(Student student)
        {
            Student existStudent = await _dbcontext.Students.FindAsync(student.StudentID);
            existStudent.AcademicPerformance10 = student.AcademicPerformance10 ?? existStudent.AcademicPerformance10;
            existStudent.AcademicPerformance11 = student.AcademicPerformance11 ?? existStudent.AcademicPerformance11;
            existStudent.AcademicPerformance12 = student.AcademicPerformance12 ?? existStudent.AcademicPerformance12;
            existStudent.Address = student.Address;
            if (student.HighSchoolID != Guid.Empty)
            {
                existStudent.HighSchoolID = student.HighSchoolID;
            }
            existStudent.IndentityCard = student.IndentityCard ?? existStudent.IndentityCard;
            existStudent.CandidateType = student.CandidateType ?? existStudent.CandidateType;
            existStudent.Ethnic = student.Ethnic ?? existStudent.Ethnic;
            existStudent.Conduct10 = student.Conduct10 ?? existStudent.Conduct10;
            existStudent.Conduct11 = student.Conduct11 ?? existStudent.Conduct11;
            existStudent.Conduct12 = student.Conduct12 ?? existStudent.Conduct12;
            existStudent.ContactRelative = student.ContactRelative ?? existStudent.ContactRelative;
            if (student.CountryID != Guid.Empty)
            {
                existStudent.CountryID = student.CountryID;
            }
            
            if (student.DateOfBirth != null)
            {
                existStudent.DateOfBirth = student.DateOfBirth;
            }
            existStudent.District = student.District ?? existStudent.District;
            existStudent.HealthStatus = student.HealthStatus ?? existStudent.HealthStatus;
            existStudent.InsuranceNumber = student.InsuranceNumber ?? existStudent.InsuranceNumber;
            existStudent.HouseholdType = student.HouseholdType ?? existStudent.HouseholdType;
            existStudent.FirstName = student.FirstName ?? existStudent.FirstName;
            existStudent.LastName = student.LastName ?? existStudent.LastName;
            existStudent.Gender = student.Gender ?? existStudent.Gender;
            if(student.JoiningDate != null)
            {
                existStudent.JoiningDate = student.JoiningDate;
            }
            existStudent.Member = student.Member ?? existStudent.Member;
            existStudent.MembershipBook = student.MembershipBook ?? existStudent.MembershipBook;
            existStudent.MembershipCard = student.MembershipCard ?? existStudent.MembershipCard;
            existStudent.Nationality= student.Nationality ?? existStudent.Nationality;
            existStudent.OutstandingAchievements = student.OutstandingAchievements ?? existStudent.OutstandingAchievements;
            existStudent.Partisan = student.Partisan ?? existStudent.Partisan;
            existStudent.PlaceIssued = student.PlaceIssued ?? existStudent.PlaceIssued;
            existStudent.PlaceJoining = student.PlaceJoining ?? existStudent.PlaceJoining;
            existStudent.PlaceOfBirth = student.PlaceOfBirth ?? existStudent.PlaceOfBirth;
            existStudent.PolicySubjectType = student.PolicySubjectType ?? existStudent.PolicySubjectType;
            existStudent.Province = student.Province ?? existStudent.Province;
            existStudent.Religion = student.Religion ?? existStudent.Religion;
            existStudent.Talent = student.Talent ?? existStudent.Talent;
            await _dbcontext.SaveChangesAsync();
        }

        // auhor id = student id
        public async Task<Student> GetAuthorByAuthorId(Guid authorID)
        {
            return await _dbcontext.Students
                        .Where(s => s.StudentID == authorID)
                        .FirstOrDefaultAsync();
        }
    }
}
