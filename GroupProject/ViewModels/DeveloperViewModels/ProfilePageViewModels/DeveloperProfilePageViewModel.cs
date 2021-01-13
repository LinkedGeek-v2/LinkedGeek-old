using GroupProject.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GroupProject.ViewModels.DeveloperViewModels.ProfilePageViewModels
{
    public class DeveloperProfilePageViewModel
    {
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName{ get; set; }

        public DateTime? DateOfBirth { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "" ,DataFormatString ="{0} years old")]     //check if convert empty string to null is needed
        public int? Age { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "", DataFormatString = "{0} at:")]
        public string CurrentJobTitle => Experiences.FirstOrDefault(ex => ex.EndYear == null).JobTitle;    


        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "")]
        public string WorksAt => Experiences.FirstOrDefault(ex => ex.EndYear == null).CompanyName;    

        public string AddressButtonName => User.Address == null ? "Add Address" : "Edit Address";

        public UserProfilePageViewModel User { get; set; }
        public List<EducationProfilePageViewModel> Educations { get; set; }
        public List<ExperienceProfilePageViewModel> Experiences { get; set; }

        public List<DeveloperSkillsProfilePageViewModel> DeveloperSkills { get; set; }

        public List<Tuple<string, string>> Actions { get; set; } = new List<Tuple<string, string>>
        {
            new Tuple<string, string>("Add Education", "EducationForm"),
            new Tuple<string, string>("Add Experience", "ExperienceForm"),
            new Tuple<string, string>("Add Skill", "SkillForm"),
        };


        public DeveloperProfilePageViewModel()
        {
            Educations = new List<EducationProfilePageViewModel>();
            Experiences = new List<ExperienceProfilePageViewModel>();
            DeveloperSkills = new List<DeveloperSkillsProfilePageViewModel>();
        }

        public void SortExperiencesWithNullsFirst() => Experiences = Experiences.OrderByDescending(ex => ex.EndYear, new NullsFirstComparer<DateTime?>()).ToList();    
        public void SortEducationsWithNullsFirst() => Educations = Educations.OrderByDescending(ex => ex.EndYear, new NullsFirstComparer<DateTime?>()).ToList();    
    }
}