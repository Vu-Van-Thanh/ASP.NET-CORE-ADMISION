using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Admission.Core.DTO
{
	public class LoginDTO
	{
		[Required(ErrorMessage = "Email không thể để trống!")]
		[EmailAddress(ErrorMessage = "Email không đúng định dạng!")]
		[DataType(DataType.EmailAddress)]
		public string? Email { get; set; }


		[Required(ErrorMessage = "Password không thể để trống!")]
		[DataType(DataType.Password)]
		public string? Password { get; set; }
	}
}
