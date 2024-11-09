using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Domain.Entities
{
    public class Group
    {
        [Key]
        public Guid GroupID { get; set; }

        [StringLength(200)]
        public string Name { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
