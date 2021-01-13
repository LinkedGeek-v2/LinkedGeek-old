using System;
using System.ComponentModel.DataAnnotations;

namespace GroupProject.ApiModels.CompanyDTOs
{
    public class CompanyDetailsPostDto
    {
        [Required(ErrorMessage = "You must enter your company's name!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = ("Company name must be between 2 and 100 characters!"))]
        public string CompanyName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}", NullDisplayText = "--")]
        public DateTime? FoundationDate { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = ("Founder name must be between 2 and 100 characters!"))]
        public string FounderName { get; set; }

        public string Description { get; set; }

    }
}