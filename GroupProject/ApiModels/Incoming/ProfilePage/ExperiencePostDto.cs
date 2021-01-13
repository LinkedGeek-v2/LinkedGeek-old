using GroupProject.CustomValidations;
using GroupProject.Enums;
using GroupProject.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace GroupProject.ApiModels.Incoming.ProfilePage
{
    public class ExperiencePostDto
    {
        public int ExperienceID { get; set; }

        [Required(ErrorMessage = "You must enter a job title!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Job title must be betweeen 2 and 100 characters!")]
        public string JobTitle { get; set; }

        public string CompanyWorkingID{ get; set; }

        [Required(ErrorMessage = "You must enter a company name or select from the list below!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Company name must be betweeen 2 and 100 characters!")]
        public string CompanyName{ get; set; }

        [DataType(DataType.DateTime)]
        [DatesDefaultValue]
        public DateTime StartYear { get; set; }

        [DataType(DataType.DateTime)]
        [FutureDate("StartYear")]
        [DatesDefaultValue]
        public DateTime? EndYear { get; set; }
        public WorkingType ExperienceType { get; set; }
        public string DeveloperID { get; set; }
    }
}