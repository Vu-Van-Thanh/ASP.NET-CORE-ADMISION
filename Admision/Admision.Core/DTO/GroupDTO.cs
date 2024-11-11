using Admission.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.DTO
{
    public class GroupDTO
    {
        public Guid GroupID { get; set; }
        public string Name { get; set; }
        public List<PostDTO> Posts { get; set; }

    }
    public static class GroupExtension
    {
        public static GroupDTO ToGroupDTO(this Group group)
        {
            return new GroupDTO { GroupID = group.GroupID, Name = group.Name };
        }
    }

}
