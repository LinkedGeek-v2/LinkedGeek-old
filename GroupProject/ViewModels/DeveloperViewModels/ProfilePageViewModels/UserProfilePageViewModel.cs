using GroupProject.Models.AssociativeModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GroupProject.ViewModels.DeveloperViewModels.ProfilePageViewModels
{
    public class UserProfilePageViewModel
    {
        public string UserName { get; set; }

        public AddressProfilePageViewModel Address { get; set; }
        public List<Following> Followers { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "" , DataFormatString = ("{0} Followers"))] 
        public int? FollowersCount => Followers.Count;
    }
}