using System;
using System.ComponentModel.DataAnnotations;

namespace GroupProject.CustomValidations
{
    public class FutureDate : ValidationAttribute
    {
        private readonly string propertyName;

        public FutureDate(string propertyName)
        {
            this.propertyName = propertyName;
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyInfo = validationContext.ObjectType.GetProperty(propertyName);
            var startDate = (DateTime)propertyInfo.GetValue(validationContext.ObjectInstance);
            var endDate = (DateTime?)value;
            if (endDate == null)
                return ValidationResult.Success;

            return endDate > startDate ? ValidationResult.Success : new ValidationResult("The End Date must be greater than the Start Date");
        }
    }
}