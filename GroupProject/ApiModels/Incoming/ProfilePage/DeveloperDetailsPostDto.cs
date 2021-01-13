using GroupProject.CustomValidations;
using GroupProject.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace GroupProject.ApiModels.Incoming.ProfilePage
{
    public class DeveloperDetailsPostDto
    {
        [Required(ErrorMessage = "You must enter a last name!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = ("Last name must be between 3 and 50 letters!"))]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You must enter a last name!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = ("Last name must be between 3 and 50 letters!"))]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DatesDefaultValue]
        public DateTime? DateOfBirth { get; set; }

        public Gender? Gender { get; set; }
    }
}