using GroupProject.Images;
using GroupProject.Models.AssociativeModels;
using GroupProject.Models.CompanyModels;
using GroupProject.Models.DeveloperModels;
using GroupProject.Models.SharedModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GroupProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Address Address { get; set; }
        public string ImageName { get; set; }

        public ICollection<Following> Followers { get; set; }
        public ICollection<Following> Followees { get; set; }
        public Developer Developer { get; set; }
        public Company Company { get; set; }
        public bool IsDeveloper { get; set; }

        public ApplicationUser()
        {
            Followees = new List<Following>();
            Followers = new List<Following>();
        }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("IsDeveloper", this.IsDeveloper.ToString()));
            return userIdentity;
        }

        public bool Follow(ApplicationUser user)
        {
            if (Followees.Any(F => F.FolloweeID == user.Id))
                return false;

            Followees.Add(new Following(this, user));

            return true;
        }

        public bool UnFollow(ApplicationUser user)
        {
            int count = Followees.Count;
            var following = Followees.SingleOrDefault(f => f.FolloweeID == user.Id);
            if (following == null)
                return false;

            Followees.Remove(following);

            return true;
        }

        public string GetUserPhotoPath()
        {
            if (ImageName == null)
            {
                if (IsDeveloper)
                {
                    if (Developer.Gender == Enums.Gender.Male)
                        return ImageHelper.GenericMaleUserImage;
                    else
                        return ImageHelper.GenericFemaleUserImage;
                }
                else
                {
                    return ImageHelper.GenericCompanyUserImage;
                }                  
            }

            return ImageName;
        }
    }
}