using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Domain.Entities
{
	public class Major
	{
		[Key]
		public Guid MajorId { get; set; }
		public Guid SchoolID { get; set; }

		[StringLength(100)]
		public string? Name { get; set; }

		[ForeignKey("SchoolID")]
		public virtual School? School { get; set; }

		public virtual ICollection<InformationOfApplied>? InformationOfApplieds { get; set; }
	}
}
