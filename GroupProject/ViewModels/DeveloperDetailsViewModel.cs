using GroupProject.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroupProject.ViewModels
{
    public class DeveloperDetailsViewModel
    {
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You must enter a last name!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = ("Last name must be between 3 and 50 letters!"))]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You must enter a last name!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = ("Last name must be between 3 and 50 letters!"))]
        public string LastName { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}", NullDisplayText = "--")]
        public DateTime? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
    }
}