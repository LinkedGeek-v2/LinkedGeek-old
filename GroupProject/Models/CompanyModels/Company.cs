using GroupProject.Models.DeveloperModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupProject.Models.CompanyModels
{
    public class Company
    {
        [Key]
        [ForeignKey("User")]
        public string CompanyID { get; set; }

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

        public ICollection<Job>  JobsPosted { get; set; }
        public ICollection<Developer> Workers { get; set; }
        public ApplicationUser User { get; set; }
    }
}