using AutoMapper;
using GroupProject.ApiModels.Incoming.ProfilePage;
using GroupProject.CustomValidations;
using GroupProject.Enums;
using GroupProject.Models.CompanyModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupProject.Models.DeveloperModels
{
    public class Experience
    {
        public int ExperienceID { get; private set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "You must enter a job title!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Job title must be betweeen 2 and 100 characters!")]
        public string JobTitle { get; private set; }

        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "You must enter a company name!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Company name must be betweeen 2 and 100 characters!")]
        public string CompanyName { get; private set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}", NullDisplayText = "--")]
        public DateTime StartYear { get; private set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}", NullDisplayText = "--")]
        [FutureDate("StartYear")]
        public DateTime? EndYear { get; private set; }

        [Display(Name = "Type")]
        public WorkingType ExperienceType { get; private set; }

       
        [ForeignKey("CompanyWorking")]
        public string CompanyWorkingId { get; set; }
        public Company CompanyWorking { get; set; }

        public string DeveloperID { get; private set; } 
        public Developer Developer { get; set; }

        private Experience() { }

        public Experience(int experienceID)
        {
            ExperienceID = experienceID;
        }

   
        public static Experience Create(ExperiencePostDto experiencePostDto, string UserID)
        {
            experiencePostDto.DeveloperID = UserID;

            return Mapper.Map<ExperiencePostDto, Experience>(experiencePostDto);
        }


        /// <summary>
        /// Creates an Experience object to mark its state as Deleted.
        /// </summary>
        /// <param name="experienceID"></param>
        /// <returns></returns>
        public static Experience Delete(int experienceID) => new Experience(experienceID);
    }
}