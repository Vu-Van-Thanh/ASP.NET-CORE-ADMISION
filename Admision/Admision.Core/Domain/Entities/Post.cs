using CsvHelper.Configuration.Attributes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Domain.Entities
{
	public class Post
	{
		[Key]
		public Guid PostID { get; set; }

		public Guid AuthorID { get; set; }	

		public DateTime DateCreated { get; set; }

		public int LikeCount { get; set; }

		[ForeignKey("AuthorID")]
		public virtual Student? Author { get; set; }

		public virtual ICollection<Comment> Comments { get; set; }

	}
}
