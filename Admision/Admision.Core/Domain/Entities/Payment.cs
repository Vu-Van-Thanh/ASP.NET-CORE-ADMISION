using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Domain.Entities
{
	public class Payment
	{
		[Key]
		public Guid PaymentID { get; set; }

		public Guid ResultID { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal InitialExpenses { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal CostPaid { get; set; }

		[ForeignKey("ResultID")]
		public virtual Result? Result { get; set; }
	}
}
