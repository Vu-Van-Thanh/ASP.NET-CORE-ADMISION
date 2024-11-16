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

		public Guid GroupID { get; set; }

        public string? Content { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

		public int LikeCount { get; set; } = 0;

		[ForeignKey("AuthorID")]
		public virtual Student? Author { get; set; }

        [ForeignKey("GroupID")]
        public virtual Group? Group { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<PostMedia> PostMedias { get; set; }

    }
}
