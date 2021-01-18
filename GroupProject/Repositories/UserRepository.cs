using GroupProject.DAL;
using GroupProject.Images;
using GroupProject.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GroupProject.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationDbContext context;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<ApplicationUser> GetWorkers(string companyId)
        {
            var workers = context.Users
                .Include(u => u.Developer)
                .Include(u => u.Developer.Experiences)
                .Where(u => u.Developer.Experiences.Any(ex => ex.CompanyWorkingId == companyId))
                .ToList();

            return workers;
        }


        public IEnumerable<ApplicationUser> Followees(string id)//works without id?
        {
            var Followees = context.Followings
                .Where(f => f.FollowerID == id)
                .Select(f => f.Followee)
                .Include(followee => followee.Developer.CompanyWorking)
                .Include(followee => followee.Company);
            return Followees;
        }

        public IEnumerable<ApplicationUser> Followers(string id = null)
        {
            var Followers = context.Followings
                .Where(f => f.Followee.Id == id)
                .Select(f => f.Follower)
                .Include(f => f.Developer.CompanyWorking)
                .Include(f => f.Company);
            return Followers.ToList();
        }

        public IEnumerable<ApplicationUser> StrongOfStrongFollowees(string id) //recommended Users of a user cant be queried for a specific ID...
        {
            //Get all followees that follow me back (STRONG FRIENDSHIP)
            var StrongFriends = GetStrongFriends(id);

            // now get all the strong friends of my strong friends (stored in a list of lists)
            var StrongOfStrong2 = StrongFriends.Select(GetStrongFriends);

            // flatten the list of lists to a single and distinct list with all the strong friends of my strongfriends
            var flattened = StrongOfStrong2.SelectMany(p => p).Distinct().ToList();
            //flattened needs to be executed (toList) because we need to compare it against my followees

            //The list may contain some of my followees and even my self.
            //Remove all unwanted users

            var myFollowees = Followees(id);

            var recommended = flattened.Where(flat => myFollowees.All(myF => myF.Id != flat.Id) && flat.Id != id);
            return recommended.ToList();
        }

        private IQueryable<ApplicationUser> GetStrongFriends(ApplicationUser user)
        {
            var id = user.Id;
            var StrongFollowees = context.Followings
                .Where(f => f.FolloweeID == id && context.Followings.Any(wings => wings.FolloweeID == f.FollowerID && wings.FollowerID == id))
                .Select(f => f.Follower)
                .Include(followee => followee.Developer.CompanyWorking)
                .Include(followee => followee.Company);

            return StrongFollowees;
        }

        public IEnumerable<ApplicationUser> GetStrongFriends(string id)
        {
            var StrongFollowees = context.Followings
                .Where(f => f.FolloweeID == id && context.Followings.Any(wings => wings.FollowerID == f.FollowerID))
                .Select(f => f.Follower)
                .Include(followee => followee.Developer.CompanyWorking)
                .Include(followee => followee.Company);

            return StrongFollowees.ToList();
        }

        public IQueryable<ApplicationUser> AllFolloweesExceptMine(string UserId)
        {
            var Followees = context.Followings
                .Where(F => F.FollowerID == UserId)
                .Select(Fol => Fol.Followee)
                .Include(wee => wee.Company)
                .Include(wee => wee.Developer.CompanyWorking);

            var Recommended = context.Users
                .Include(u => u.Developer.CompanyWorking)
                .Include(u => u.Company)
                .Where(u => !Followees.Contains(u) && u.Id != UserId);

            return Recommended;
        }

        public bool UnFollow(string followerId, string followeeId)
        {
            var currUser = context.Users.Include(u => u.Followees).SingleOrDefault(u => u.Id == followerId);

            //var removedUser = context.Followings.Select(f=>f.Followee).SingleOrDefault(followee=>followee.Id==id);
            var removedUser = context.Users.SingleOrDefault(u => u.Id == followeeId);
            if (removedUser == null)
                return false;

            currUser.UnFollow(removedUser);

            return true;
        }

        public bool Follow(string followerId, string followeeId) //works without include followees???? check
        {
            var currUser = context.Users.Include(u => u.Followees).SingleOrDefault(u => u.Id == followerId);
            var addedUser = context.Users.SingleOrDefault(u => u.Id == followeeId);
            if (addedUser == null)
                return false;

            var followed = currUser.Follow(addedUser);

            return true;
        }

        public void SavePic(string userId, string ImageBase64)
        {
            var user = context.Users.SingleOrDefault(u => u.Id == userId);
            string newImageName = "";
            if (user.ImageName != null)
                newImageName = ImageHelper.SaveUserImage(ImageBase64, user.ImageName);
            else
                newImageName = ImageHelper.SaveUserImage(ImageBase64);

            user.ImageName = newImageName;

            context.SaveChanges();
        }


        public void Dispose()
        {
            context.Dispose();
        }
    }
}