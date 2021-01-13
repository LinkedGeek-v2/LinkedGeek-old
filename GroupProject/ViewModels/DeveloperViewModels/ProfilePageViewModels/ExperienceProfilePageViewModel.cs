using GroupProject.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GroupProject.ViewModels.DeveloperViewModels.ProfilePageViewModels
{
    public class ExperienceProfilePageViewModel
    { 
        public int ExperienceID { get; set; }

        [Display(Name = "Job Title:")]
        [Required(ErrorMessage = "You must enter a job title!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Job title must be betweeen 2 and 100 characters!")]
        public string JobTitle { get; set; }

        [Display(Name = "Company Name:")]
        public string CompanyName { get; set; }

        [Display(Name = "Started In: ")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime StartYear { get; set; }

        public string CompanyWorkingID { get; set; }

        public List<CompanyNamesForDeveloperProfileViewModel> CompaniesToChoose { get; set; }

        [Display(Name = "Until: ")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}", NullDisplayText = "Present")]
        public DateTime? EndYear { get; set; }

        [Display(Name="Job Type:")]
        public WorkingType ExperienceType { get; set; }

        public ExperienceProfilePageViewModel()
        {
            CompaniesToChoose = new List<CompanyNamesForDeveloperProfileViewModel>();
        }
    }
}