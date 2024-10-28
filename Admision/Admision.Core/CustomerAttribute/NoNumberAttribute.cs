using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Admission.Core.CustomerAtribute
{
    public class NoNumberAttribute:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var name = value.ToString();
            // Kiểm tra nếu chuỗi chứa số
            if (Regex.IsMatch(name, @"\d"))
            {
                return new ValidationResult("Tên không được chứa số.");
            }

            return ValidationResult.Success;
        }
    }
}
