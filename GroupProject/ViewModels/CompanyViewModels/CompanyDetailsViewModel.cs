using System;
using System.ComponentModel.DataAnnotations;

namespace GroupProject.ViewModels.CompanyViewModels
{
    public class CompanyDetailsViewModel
    {
        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "You must enter your company's name!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = ("Company name must be between 2 and 100 characters!"))]
        public string CompanyName { get; set; }

        [Display(Name = "Date Founded")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}", NullDisplayText = "--")]
        public DateTime? FoundationDate { get; set; }

        [Display(Name = "Founder")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ("Founder name must be between 2 and 100 characters!"))]
        public string FounderName { get; set; }

        public string Description { get; set; }
    }
}