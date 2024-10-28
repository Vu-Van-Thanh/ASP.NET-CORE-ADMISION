using Admission.Core.CustomerAtribute;
using Admission.Core.CustomerAttribute;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.DTO
{
    public class RegisterExDTO
    {
        [StringLength(40)]
        [Required(ErrorMessage = "Tên không được để trống!")]
        [NoNumber]
        public string FullName { get; set; }

        public GenderOptions Gender { get; set; }

        [Required(ErrorMessage = "Căn cước công dân là bắt buộc.")]
        [StringLength(12, ErrorMessage = "Căn cước công dân không được vượt quá 12 ký tự.")]
        [RegularExpression(@"^\d{9,12}$", ErrorMessage = "Căn cước công dân phải là số và có độ dài từ 9 đến 12 ký tự.")]
        public string IdentityCardNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(200)] //nvarchar(200)
        public string? Address { get; set; }

        public string AdmissionMethod { get; set; }
        public string HighSchool { get; set; }
        public string GraduatedYear { get; set; }

        [GPA]
        [Required(ErrorMessage = "GPA là bắt buộc!")]
        public double GPA10 { get; set; }
        [GPA]
        [Required(ErrorMessage = "GPA là bắt buộc!")]
        public double GPA11 { get; set; }
        [GPA]
        [Required(ErrorMessage = "GPA là bắt buộc!")]
        public double GPA12 { get; set; }

        [Required(ErrorMessage = "Email không được để trống!")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng!")]
        public string Email { get; set; }


        [Required(ErrorMessage = "SĐT không được để trống!")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Số điện thoại chỉ được chứa kí tự số!")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Ảnh chân dung là bắt buộc.")]
        public string PortraitPath { get; set; } // Đường dẫn tới ảnh chân dung

        [Required(ErrorMessage = "Ảnh căn cước là bắt buộc.")]
        public string IdentityCardBackSide{ get; set; } // Đường dẫn tới mặt trước ảnh căn cước
        [Required(ErrorMessage = "Ảnh căn cước là bắt buộc.")]
        public string IdentityCardFrontSide { get; set; } // Đường dẫn tới mặt trước ảnh căn cước

        [Required(ErrorMessage = "Bảng điểm là bắt buộc.")]
        public string TranscriptPath { get; set; } // Đường dẫn tới bảng điểm

    }
}
