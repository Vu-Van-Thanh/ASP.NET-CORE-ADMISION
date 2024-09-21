using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Domain.Entities
{
	public class InformationOfApplied
	{
		[Key]
		public Guid Id { get; set; }

		public Guid StudentID { get; set; }
		[StringLength(50)]
		public string? AdmissionMethod { get; set; }

		public Guid MajorID { get; set; }

		public DateTime TestDate { get; set; }

		[StringLength(10)]
		public string? TestRoom {  get; set; }

		public double GPA10 { get; set; }

		public double GPA11 { get; set; }

		public double GPA12 { get; set; }

		[ForeignKey("StudentID")]
		public virtual Student? Student { get; set; }

		[ForeignKey("MajorID")]
		public virtual Major? Major { get; set; }
	}
}
