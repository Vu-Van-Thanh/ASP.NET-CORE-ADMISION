using Admission.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.DTO
{
    public class StudentMediaDTO
    {
        public string StudentMediaType { get; set; }
        public Guid StudentID { get; set; }
        public string MediaUrl { get; set; }
    }
    public static class StudentMediaExtension
    {
        public static StudentMediaDTO ToSMDTO(this StudentMedia studentMedia)
        {
            return new StudentMediaDTO { StudentID = studentMedia.StudentID, StudentMediaType = studentMedia.StudentMediaType,MediaUrl=studentMedia.MediaUrl};
        }
    
    }

}
