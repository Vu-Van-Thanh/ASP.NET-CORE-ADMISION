using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Domain.Entities
{
    public class PostMedia
    {
        public Guid PostID { get; set; }
        public Guid MediaID { get; set; }

        public string Type { get; set; }
        public string MediaUrl { get; set; }

        [ForeignKey("PostID")]
        public virtual Post Post { get; set; }

    }
}
