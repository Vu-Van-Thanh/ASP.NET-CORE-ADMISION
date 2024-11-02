using Microsoft.AspNetCore.Mvc;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.DTO
{
	public class StudentInfoDTO
	{
		[StringLength(20)]
		public string? FirstName { get; set; }

		[StringLength(20)]
		public string? LastName { get; set; }

		public DateTime? DateOfBirth { get; set; }

		[StringLength(10)]
		public GenderOptions? Gender { get; set; }
		public Guid HighSchoolID { get; set; }

		[StringLength(200)] //nvarchar(200)
		public string? Address { get; set; }

		[StringLength(150)]
		public string? PathOfAvatar { get; set; } = "default_avatar.jpg";


		[EmailAddress(ErrorMessage = "Email không đúng định dạng!")]
		public string Email { get; set; }


		[RegularExpression("^[0-9]*$", ErrorMessage = "Số điện thoại chỉ được chứa kí tự số!")]
		[DataType(DataType.PhoneNumber)]
		public string Phone { get; set; }


	}
}
