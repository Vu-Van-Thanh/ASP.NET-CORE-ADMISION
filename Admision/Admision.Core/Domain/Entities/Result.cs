using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Domain.Entities
{
	public class Result
	{
		[Key]
		public Guid ResultId { get; set; }

		public Guid InfoAppliedID { get; set; }

		[StringLength(10)]
		public string? Status {  get; set; }

		public double? Score { get; set; }

		[ForeignKey("InfoAppliedID")]
		public virtual InformationOfApplied? Info { get; set; }
	}
}
