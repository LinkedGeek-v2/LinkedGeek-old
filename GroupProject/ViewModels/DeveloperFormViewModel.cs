using System;
using System.ComponentModel.DataAnnotations;
using GroupProject.Enums;

namespace GroupProject.ViewModels
{
    public class DeveloperFormViewModel
    {
        [Required(ErrorMessage = "You must enter a first name!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = ("First name must be between 3 and 50 letters!"))]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You must enter a last name!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = ("Last name must be between 3 and 50 letters!"))]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        public Gender? Gender { get; set; }
    }
}