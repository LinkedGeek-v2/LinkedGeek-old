using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GroupProject.ViewModels.CompanyViewModels
{
    public class CompanyFormViewModel
    {
        public string CompanyID { get; set; }

        [Required(ErrorMessage = "You must enter your company's name!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = ("Company name must be between 2 and 100 characters!"))]
        public string CompanyName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FoundationDate { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = ("Founder name must be between 2 and 100 characters!"))]
        public string FounderName { get; set; }

        public string Description { get; set; }

        public readonly IEnumerable<string> Actions = new string[]
        {
            "Home",
            "About",
            "Jobs",
            "People",
            "Applicants"
        };

        public CompanyFormViewModel()
        { }

        public CompanyFormViewModel(string companyId)
        {
            CompanyID = companyId;
        }
    }
}