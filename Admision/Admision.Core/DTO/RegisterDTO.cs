using Admission.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Admission.Core.DTO
{
 public class RegisterDTO
 {
  [Required(ErrorMessage = "Tên người dùng không được để trống!")]
  public string PersonName { get; set; }


  [Required(ErrorMessage = "Email không được để trống!")]
  [EmailAddress(ErrorMessage = "Email không đúng định dạng!")]
  [Remote(action: "IsEmailAlreadyRegistered", controller: "Account", ErrorMessage = "Email đã được sử dụng")] 
  public string Email { get; set; }


  [Required(ErrorMessage = "SĐT không được để trống!")]
  [RegularExpression("^[0-9]*$", ErrorMessage = "Số điện thoại chỉ được chứa kí tự số!")]
  [DataType(DataType.PhoneNumber)]
  public string Phone { get; set; }


  [Required(ErrorMessage = "Mật khẩu không được để trống!")]
  [DataType(DataType.Password)]
  public string Password { get; set; }


  [Required(ErrorMessage = "Xác nhận MK không được để trống!")]
  [DataType(DataType.Password)]
  [Compare("Password", ErrorMessage = "Mật khẩu và Xác nhận MK không giống nhau!")]
  public string ConfirmPassword { get; set; }

  public UserTypeOptions UserType { get; set; } = UserTypeOptions.User;
 }
}
