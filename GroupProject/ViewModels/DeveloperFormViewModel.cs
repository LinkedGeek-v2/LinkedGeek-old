using System;
using System.ComponentModel.DataAnnotations;
using GroupProject.Enums;

namespace GroupProject.ViewModels
{
    public class DeveloperFormViewModel
    {
        public string DeveloperID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You must enter a first name!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = ("First name must be between 3 and 50 letters!"))]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You must enter a last name!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = ("Last name must be between 3 and 50 letters!"))]
        public string LastName { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? DateOfBirth { get; set; }

        public Gender? Gender { get; set; }

        public DeveloperFormViewModel()
        { }

        public DeveloperFormViewModel(string developerId)
        {
            DeveloperID = developerId;
        }
    }
}