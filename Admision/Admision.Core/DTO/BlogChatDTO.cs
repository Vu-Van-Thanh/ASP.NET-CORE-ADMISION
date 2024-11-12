using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.DTO
{
    public class BlogChatDTO
    {
        public List<GroupDTO> groups {  get; set; }
        public List<PostDTO> posts { get; set; }
    }
}
