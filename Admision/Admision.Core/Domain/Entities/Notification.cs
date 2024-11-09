using Admission.Core.Domain.IdentityEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Domain.Entities
{
    public class Notification
    {
        [Key]
        public Guid NoId { get; set; }
        public Guid GroupId { get; set; }
        public Guid UserId { get; set; }  

        public Guid? PostId { get; set; }

        [StringLength(40)]
        public string NotificationType { get; set; }  

        public bool HasNewPosts { get; set; }
        public DateTime LastChecked { get; set; } = DateTime.Now;

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }

    }
}
