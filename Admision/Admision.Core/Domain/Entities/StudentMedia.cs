using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Domain.Entities
{
    public class StudentMedia
    {
        
        public Guid StudentMediaID { get; set; }

        public string StudentMediaType { get; set; }
        public Guid StudentID { get; set; }
        public string MediaUrl { get; set; }

        [ForeignKey("StudentID")]
        public virtual Student student { get; set; }



        // các media cần có
        /*[StringLength(150)]
        public string? PathOfAvatar { get; set; } = "default_avatar.jpg";

        // căn cước 
        public string? FrontICUrl { get; set; }
		public string? LastICUrl { get; set; }

        //BHYT
        public string? FrontINUrl { get; set; }
        public string? LastINUrl { get; set; }

        // Hộ khẩu Household registration photo
        public List<string>? HRPUrl { get; set; }

        // học bạ
        public string? TranscriptUrl10.1 { get; set; }
        public string? TranscriptUrl10.2 { get; set; }
        public string? TranscriptUrl11.1 { get; set; }
        public string? TranscriptUrl11.2 { get; set; }
        public string? TranscriptUrl12.1 { get; set; }
        public string? TranscriptUrl12.2 { get; set; }

        // các ảnh khác
        public string? THPTCertificateUrl {get;set}
        public string? BirthCertificateUrl {get;set}
        public string? TransferPaperUrl {get;set}
        public string? MilitaryServiceDefermentCertificateUrl {get;set}




        */
    }
}
