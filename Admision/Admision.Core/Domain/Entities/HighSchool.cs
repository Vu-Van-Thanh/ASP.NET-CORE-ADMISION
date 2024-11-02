using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Domain.Entities
{
	public class HighSchool
	{

		[Key]
		public Guid HighSchoolID { get; set; }

		[StringLength(100)]
		public string HighSchoolName { get; set; }


		[StringLength(100)]
		public string? DepartmentOfEducation { get; set; }


		[StringLength(10)]
		public string? Type { get; set; }

		[StringLength(200)]
		public string? Address { get; set; }
		// Navigation property
		public virtual ICollection<Student> Students { get; set; }


	}
}
