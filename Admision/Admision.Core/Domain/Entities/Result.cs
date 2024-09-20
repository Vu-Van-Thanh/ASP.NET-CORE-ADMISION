using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admision.Core.Domain.Entities
{
	public class Result
	{
		[Key]
		public Guid ResultId { get; set; }

	}
}
