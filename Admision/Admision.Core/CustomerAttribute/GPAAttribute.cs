using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.CustomerAttribute
{

    public class GPAAttribute:ValidationAttribute
    {
        private readonly double _minGpa;
        private readonly double _maxGpa;

        public GPAAttribute(double minGpa = 0.0, double maxGpa = 10.0)
        {
            _minGpa = minGpa;
            _maxGpa = maxGpa;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            if (double.TryParse(value.ToString(), out double gpa))
            {

                if (gpa < _minGpa || gpa > _maxGpa)
                {
                    return new ValidationResult($"GPA phải nằm trong khoảng {_minGpa} đến {_maxGpa}.");
                }
                return ValidationResult.Success;
            }

            return new ValidationResult("Giá trị GPA không hợp lệ.");
        }

    }
}
