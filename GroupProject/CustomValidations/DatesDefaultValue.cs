using System;
using System.ComponentModel.DataAnnotations;

namespace GroupProject.CustomValidations
{
    public class DatesDefaultValue:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value!=null && value.GetType() == typeof(DateTime))
            {             
                   var startYear = (DateTime)value;
                   if (startYear < DateTime.Parse("1900/1/1")) return new ValidationResult("Date should be sooner than 1900!");               
            }
  
            return ValidationResult.Success;
        }
    }
}