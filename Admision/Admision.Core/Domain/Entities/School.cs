using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Domain.Entities
{
	public class School
	{
		public Guid SchoolID { get; set; }

		[StringLength(200)]
		public string? SchoolName { get; set; }

		[StringLength(300)]
		public string? SchoolDescription { get; set; }

		[StringLength(10)]
		public string? Location { get; set; }

		[StringLength(30)]
		public string? WebSite { get; set; }

		public int PhoneNumber { get; set; }

		[StringLength(40)]
		public string? Email {  get; set; }
	}
}
