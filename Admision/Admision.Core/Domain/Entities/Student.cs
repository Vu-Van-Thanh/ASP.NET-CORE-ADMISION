using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admission.Core.Domain.IdentityEntities;
using ServiceContracts.Enums;

namespace Admission.Core.Domain.Entities
{
	public class Student
	{
		[Key]
		public Guid StudentID { get; set; }

		[StringLength(20)] 
		public string? FirstName { get; set; }

		[StringLength(20)]
		public string? LastName { get; set; }

		public DateTime? DateOfBirth { get; set; }

		[StringLength(10)]
		public GenderOptions? Gender { get; set; }

		public Guid HighSchoolID { get; set; }
		public Guid CountryID { get; set; } = Guid.Parse("4ac574fb-85a9-4159-9f2b-053b2bc6fc7e");

        // AccountID
        public Guid Id { get; set; }

		[StringLength(200)] //nvarchar(200)
		public string? Address { get; set; }


		[ForeignKey("HighSchoolID")]
		public virtual HighSchool? HighSchool { get; set; }

		public virtual ApplicationUser? ApplicationUser { get; set; }


		/// <summary>
		/// Navigation property
		/// </summary>
		public virtual ICollection<InformationOfApplied>? InformationOfApplieds { get; set; }
		public virtual ICollection<Post>? Posts { get; set; }
		public virtual ICollection<Comment>? Comments { get; set; }
		public virtual ICollection<Result>? Results { get; set; }
		public virtual ICollection<Relative>? Relatives { get; set; }
		public virtual ICollection<StudentMedia>? StudentMedias { get; set; }

        // Thông tin bổ sung
        [StringLength(100)]
		public string? Nationality { get; set; } = "Việt Nam";
        [StringLength(30)]
        public string? Ethnic { get; set; }

        [StringLength(50)]
        public string? Religion { get; set; } = "Không";

		public string? PlaceOfBirth { get; set; }

		// căn cước công dân
		public string? IndentityCard { get; set; }
        public string? PlaceIssued { get; set; }

		

		// hộ khẩu thường trú
		[ForeignKey("CountryID")]
        public Country? Country { get; set; }
        [StringLength(40)]
        public string? Province { get; set; }
        [StringLength(40)]
        public string? District { get; set; }
        [StringLength(40)]
        public string? Commune { get; set; }

		// Người liên hệ
		[NotMapped]
		public Relative? ContactRelative { get; set; }

		public string? CandidateType { get; set; }

		// đoàn viên
		[StringLength(20)]
		public string? Member { get; set; }
		[StringLength(20)]
		public string? Partisan { get; set; }

		// ngày vào đoàn
		public DateTime JoiningDate { get; set; }

		// nơi vào đoàn
		[StringLength(200)]
		public string? PlaceJoining {  get; set; }

        // sổ đoàn viên
        [StringLength(20)]
        public string? MembershipBook { get; set; }
        // thẻ đoàn viên
        [StringLength(20)]
        public string? MembershipCard { get; set; }

        // sức khỏe
        [StringLength(50)]
		public string? HealthStatus { get; set; }

		[StringLength(30)]
		public string? InsuranceNumber { get; set; }
        
        

		// đối tượng chính sách
		[StringLength(100)]
		public string? PolicySubjectType { get; set; }
		//Hộ gia đình
		[StringLength(20)]
		public string? HouseholdType { get; set; }


		// Học lực
		[StringLength(20)]
		public string? AcademicPerformance10 { get; set; }
        [StringLength(20)]

        public string? Conduct10 { get; set; }
        [StringLength(20)]

        public string? AcademicPerformance11 { get; set; }
        [StringLength(20)]

        public string? Conduct11 { get; set; }
        [StringLength(20)]

        public string? AcademicPerformance12 { get; set; }
        [StringLength(20)]

        public string? Conduct12 { get; set; }

		[StringLength(200)]
		public string? OutstandingAchievements { get; set; }

        // thông tin khác
        [StringLength(150)]
        public string? Talent {  get; set; }



        public override string ToString()
		{
			return $"Person ID: {StudentID}, Person Name: {FirstName + LastName}, Date of Birth: {DateOfBirth?.ToString("MM/dd/yyyy")}, Gender: {Gender}, Address: {Address}";
		}
	}
}
