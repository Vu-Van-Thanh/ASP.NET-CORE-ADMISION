using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		public string? Gender { get; set; }

		public Guid HighSchoolID { get; set; }


		[StringLength(200)] //nvarchar(200)
		public string? Address { get; set; }


		[ForeignKey("HighSchoolID")]
		public virtual HighSchool? HighSchool { get; set; }

		public override string ToString()
		{
			return $"Person ID: {StudentID}, Person Name: {FirstName + LastName}, Date of Birth: {DateOfBirth?.ToString("MM/dd/yyyy")}, Gender: {Gender}, Address: {Address}";
		}
	}
}
