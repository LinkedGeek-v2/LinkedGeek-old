using GroupProject.Enums;
using GroupProject.Models.AssociativeModels;
using GroupProject.Models.CompanyModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupProject.Models.DeveloperModels
{
    public class Developer
    {
        [Key]
        [ForeignKey("User")]
        public string DeveloperID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please enter your first name!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = ("First name must be between 3 and 50 letters!"))]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please enter your last name!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = ("Last name must be between 3 and 50 letters!"))]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}", NullDisplayText = "--")]
        public DateTime? DateOfBirth { get; set; }

        public int? Age
        {
            get
            {
                if (DateOfBirth.HasValue)
                {
                    return DateTime.Now.Year - DateOfBirth.Value.Year;
                }

                return null;
            }
        }


        [DisplayFormat(NullDisplayText = "--")]
        public Gender? Gender { get; set; }

        [ForeignKey("CompanyWorking")]
        public string CompanyWorkingId { get; set; }
        public Company CompanyWorking { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<Education> Educations { get; set; }
        public ICollection<Experience> Experiences { get; set; }
        public ICollection<DeveloperSkills> DeveloperSkills { get; set; }
        public ICollection<JobsApplied> JobsApplied { get; set; }
    }
}