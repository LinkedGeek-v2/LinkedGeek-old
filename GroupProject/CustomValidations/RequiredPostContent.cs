using GroupProject.ApiModels.PostsDTOs;
using System.ComponentModel.DataAnnotations;

namespace GroupProject.CustomValidations
{
    public class RequiredPostContent : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var post = (IncomingPostDto) validationContext.ObjectInstance;

            if (post.ImageBase64 == null && post.Text == null)
                return new ValidationResult("The post wasn't submitted. A text or a picture is required.");

            return ValidationResult.Success;
        }
    }
}