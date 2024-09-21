using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Domain.Entities
{
	public class Article
	{
		[Key]
		public Guid ArticleId { get; set; }

		[StringLength(20)]
		public string? Title { get; set; }

		[StringLength(100)]				
		public string? Content { get; set; }
		public DateTime DateCreated { get; set; }
	}
}
