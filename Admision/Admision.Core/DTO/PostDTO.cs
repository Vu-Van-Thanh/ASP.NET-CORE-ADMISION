using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.DTO
{
    public class PostDTO
    {
        public PostContentDTO content { get ; set; }
        public List<PostMediaDTO> media { get; set; }
    
    }
}
