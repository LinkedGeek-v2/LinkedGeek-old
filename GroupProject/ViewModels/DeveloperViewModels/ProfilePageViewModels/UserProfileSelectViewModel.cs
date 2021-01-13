using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GroupProject.ViewModels.DeveloperViewModels.ProfilePageViewModels
{
    public class UserProfileSelectViewModel
    {
        public string UserID{ get; set; }
        public string UserName { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "", DataFormatString = ("{0} Followers"))]
        public int? Count { get; set; }

        [Display(Name = "Street Name")]
        [Required(ErrorMessage = "Street Name is required!")]
        public string StreetName { get; set; }

        [Display(Name = "Street Number")]
        [StringLength(6, ErrorMessage = ("Street number can be between 1 and 6 characters!"), MinimumLength = 1)]
        public string StreetNumber { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "", DataFormatString = ("{0} , "))]
        public string CityName { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "")]
        public string CountryName { get; set; }

        public string FullName { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "", DataFormatString = "{0} years old")]     //check if convert empty string to null is needed
        public int? Age { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "", DataFormatString = "{0} at:")]
        public string CurrentJobTitle => Experiences.FirstOrDefault(ex => ex.EndYear == null).JobTitle;

        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "")]
        public string WorksAt => Experiences.FirstOrDefault(ex => ex.EndYear == null).CompanyName;

        public string AddressButtonName => StreetName == null ? "Add Address" : "Edit Address";

        public List<EducationProfilePageViewModel> Educations { get; set; }
        public List<ExperienceProfilePageViewModel> Experiences { get; set; }
        public List<DeveloperSkillsProfilePageViewModel> DeveloperSkills { get; set; }
    }
}