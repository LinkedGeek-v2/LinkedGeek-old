using GroupProject.ApiModels.PostsDTOs;
using System.ComponentModel.DataAnnotations;

namespace GroupProject.CustomValidations
{
    public class ImageType : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var post = (IncomingPostDto)validationContext.ObjectInstance;
            if (post.ImageBase64 != null)
                if (!post.ImageBase64.StartsWith("data:image/png") && !post.ImageBase64.StartsWith("data:image/jpeg"))
                    return new ValidationResult("Image format not supported. Only .jpg and .png allowed");

            return ValidationResult.Success;
        }
    }
}