using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Domain.Entities
{
	public class Comment
	{
		[Key]
		public Guid CommentID { get; set; }	

		public Guid PostID { get; set; }
		public Guid AuthorID { get; set; }

		public string content { get; set; }
		public DateTime CreatedDate { get; set; }
		public int LikeCount { get; set; }

		[ForeignKey("PostID")]
		public virtual Post? Post { get; set; }



		[ForeignKey("AuthorID")]
		public virtual Student? Student { get; set; }

	}
}
