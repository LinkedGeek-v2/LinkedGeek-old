using System.ComponentModel.DataAnnotations;

namespace GroupProject.ViewModels.DeveloperViewModels.ProfilePageViewModels
{
    public class CityProfilePageViewModel
    {
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "" , DataFormatString =("{0} , "))]
        public string CityName { get; set; }

        public CountryProfilePageViewModel Country { get; set; }
    }
}