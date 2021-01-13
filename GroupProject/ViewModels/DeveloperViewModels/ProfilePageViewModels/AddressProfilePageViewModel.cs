using System.ComponentModel.DataAnnotations;

namespace GroupProject.ViewModels.DeveloperViewModels.ProfilePageViewModels
{
    public class AddressProfilePageViewModel
    {
        [Display(Name = "Street Name")]
        [Required(ErrorMessage = "Street Name is required!")]
        public string StreetName { get; set; }

        [Display(Name = "Street Number")]
        [StringLength(6, ErrorMessage = ("Street number can be between 1 and 6 characters!"), MinimumLength = 1)]
        public string StreetNumber { get; set; }
        public CityProfilePageViewModel City { get; set; }
    }
}