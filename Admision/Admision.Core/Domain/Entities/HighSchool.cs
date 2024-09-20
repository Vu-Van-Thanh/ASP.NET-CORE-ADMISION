using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admision.Core.Domain.Entities
{
	public class HighSchool
	{

		[Key]
		public Guid HighSchoolID { get; set; }

		[StringLength(40)]
		public string HighSchoolName { get; set; }


		[StringLength(40)]
		public string? Province { get; set; }

		// Navigation property
		public virtual ICollection<Student> Students { get; set; }


	}
}
