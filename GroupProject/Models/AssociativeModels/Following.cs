using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupProject.Models.AssociativeModels
{
    public class Following
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Follower")]
        public string FollowerID { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Followee")]
        public string FolloweeID { get; set; }

        public ApplicationUser Follower { get; set; }
        public ApplicationUser Followee { get; set; }

        public Following()
        {

        }

        public Following(string FollowerID, string FolloweeID)
        {
            this.FollowerID = FollowerID;
            this.FolloweeID = FolloweeID;
        }

        public Following(ApplicationUser Follower, ApplicationUser Followee)
        {
            this.Follower = Follower;
            this.Followee = Followee;
            this.FollowerID = Follower.Id;
            this.FolloweeID = FolloweeID;
        }
    }
}