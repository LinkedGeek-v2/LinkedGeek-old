using System.ComponentModel.DataAnnotations;

namespace GroupProject.ViewModels.DeveloperViewModels.ProfilePageViewModels
{
    public class CountryProfilePageViewModel
    {
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "")]
        public string CountryName { get; set; }
    }
}