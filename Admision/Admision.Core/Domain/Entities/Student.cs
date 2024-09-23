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

		[StringLength(10)] //nvarchar(100)
		public GenderOptions Gender { get; set; }

		public Guid HighSchoolID { get; set; }

		// AccountID
		public Guid Id { get; set; }

		[StringLength(200)] //nvarchar(200)
		public string? Address { get; set; }


		[ForeignKey("HighSchoolID")]
		public virtual HighSchool? HighSchool { get; set; }


		[ForeignKey("Id")]
		public ApplicationUser? ApplicationUser { get; set; }


		/// <summary>
		/// Navigation property
		/// </summary>
		public virtual ICollection<InformationOfApplied>? InformationOfApplieds { get; set; }
		public virtual ICollection<Post>? Posts { get; set; }
		public virtual ICollection<Comment>? Comments { get; set; }



		public override string ToString()
		{
			return $"Person ID: {StudentID}, Person Name: {FirstName + LastName}, Date of Birth: {DateOfBirth?.ToString("MM/dd/yyyy")}, Gender: {Gender}, Address: {Address}";
		}
	}
}
