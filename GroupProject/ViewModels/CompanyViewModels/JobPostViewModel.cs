using GroupProject.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace GroupProject.ViewModels.CompanyViewModels
{
    public class JobPostViewModel
    {
        [Required]
        public string CompanyID { get; set; }
        public int JobID { get; set; }

        [Required(ErrorMessage = "Please enter your Job Title!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Job title must be between 2 and 100 characters!")]
        public string JobTitle { get; set; }
        public DateTime DatePosted { get; set; }
        public string JobDescription { get; set; }
        public WorkingType JobType { get; set; }
    }
}