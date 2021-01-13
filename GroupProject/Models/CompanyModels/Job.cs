using GroupProject.Enums;
using GroupProject.Models.AssociativeModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GroupProject.Models.CompanyModels
{
    public class Job
    {
        public int JobID { get; private set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Please enter your Job Title!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Job title must be between 2 and 100 characters!")]
        public string JobTitle { get; private set; }

        [Display(Name = "Date Posted")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DatePosted { get; private set; }

        [Display(Name = "Description")]
        public string JobDescription { get; private set; }

        [Display(Name = "Type")]
        public WorkingType JobType { get; private set; }

        public string CompanyID { get; private set; }
        public Company Company { get; set; }

        public ICollection<JobsApplied> JobsApplied { get; set; }

        private Job()
        { }

        public Job(int id)
        {
            JobID = id;
        }

        public Job(string jobTitle, string jobDescription, WorkingType jobType, string companyID)
        {
            JobTitle = jobTitle;
            DatePosted = DateTime.Now;
            JobDescription = jobDescription;
            JobType = jobType;
            CompanyID = companyID;
        }
    }
}