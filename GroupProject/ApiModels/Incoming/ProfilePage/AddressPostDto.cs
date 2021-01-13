using System.ComponentModel.DataAnnotations;

namespace GroupProject.ApiModels.Incoming.ProfilePage
{
    public class AddressPostDto
    {
        public string AddressID { get; set; }

        [Display(Name = "Street Name")]
        [Required(ErrorMessage = "Street Name is required!")]
        public string StreetName { get; set; }

        [Display(Name = "Street Number")]
        [StringLength(6,ErrorMessage =("Street number can be between 1 and 6 characters!"),MinimumLength =1)]
        public string StreetNumber { get; set; }

        [Required(ErrorMessage ="You must choose a city!")]
        public int? CityID { get; set; }

        public void MatchWithUser(string UserId)
        {
            AddressID = UserId;

        }
    }
}