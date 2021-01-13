using GroupProject.DAL;
using GroupProject.Models.SharedModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GroupProject.Repositories
{
    public class PostsRepository
    {
        private readonly ApplicationDbContext context;

        public PostsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Post Add(Post post)
        {
            context.Posts.Add(post);
            return post;
        }

        public IEnumerable<Post> GetUserSpecificPosts(string UserId)
        {
            var st=UserSpecificPosts(UserId).ToList();
            return st;
        }

        public IEnumerable<Post> GetUserSpecificPosts(string UserId, int startIndex, int endIndex)
            => UserSpecificPosts(UserId).Skip(startIndex).Take(endIndex - startIndex).ToList();

        public IEnumerable<Post> GetUserAndFolloweesPosts(string UserId)
            => UserRelatedPosts(UserId).ToList();

        //returns all the records from start index to endIndex
        public IEnumerable<Post> GetUserAndFolloweesPosts(string UserId, int startIndex, int endIndex)
            => UserRelatedPosts(UserId).Skip(startIndex).Take(endIndex - startIndex).ToList();



        private IQueryable<Post> UserSpecificPosts(string UserId)
        {
            var posts = context.Posts.Include(p => p.User)
                .Include(p => p.User.Developer.CompanyWorking)
                .Include(p => p.User.Company)
                .Where(p => p.UserId == UserId)
                .OrderByDescending(p => p.DatePosted);

            return posts;
        }


        private IQueryable<Post> UserRelatedPosts(string UserId)
        {
            //Get all my Followees
            var Users = context.Followings
                .Where(F => F.FollowerID == UserId)
                .Select(F => F.Followee)
                .Include(Followee => Followee.Developer)
                .Include(Followee => Followee.Company);

            //Get all my Followees' Posts (and mine)
            var posts = context.Posts.Include(p => p.User)
                .Include(p => p.User.Developer.CompanyWorking)
                .Include(p => p.User.Company)
                .Where(p => Users.Any(u => u.Id == p.UserId) || p.UserId == UserId)
                .OrderByDescending(p => p.DatePosted);

            return posts;
        }



        //public Post GetOutgoingPost(int PostId)
        //{
        //    var post = context.Posts.Include(p => p.User)
        //        .Include(p => p.User.Developer.CompanyWorking)
        //        .Include(p => p.User.Company)
        //        .SingleOrDefault(p => p.PostID == PostId);
        //    return post;
        //}

        //public OutgoingPostDto Add(PostDto dto, string UserId)
        //{

        //    string ImageName = String.Empty;

        //    if (dto.ImageBase64 != null)
        //        ImageName = ImageHelper.SavePostImg(dto.ImageBase64);



        //    Post post = new Post()
        //    {
        //        UserId = UserId,
        //        ImageName = ImageName != String.Empty ? ImageName : null,
        //        Text = dto.Text,
        //        DatePosted = (DateTime)dto.DatePosted
        //    };



        //    context.Posts.Add(post);
        //    context.SaveChanges();
        //    //OutgoingPostDto dto1 = GetOutgoingPost(post.PostID);
        //    OutgoingPostDto outDto = new OutgoingPostDto()
        //    {
        //        UserId = post.UserId,
        //        PostImageBase64 = post.ImageName,
        //        Text = post.Text,
        //        DatePosted = post.DatePosted
        //    };
        //    return outDto;
        //}

        //public OutgoingPostDto Add(IncomingPostDto dto, string UserId)
        //{
        //    string ImageName = String.Empty;

        //    if (dto.ImageBase64 != null)
        //        ImageName = ImageHelper.SavePostImg(dto.ImageBase64);

        //    Post post = new Post()
        //    {
        //        UserId = UserId,
        //        ImageName = ImageName != String.Empty ? ImageName : null,
        //        Text = dto.Text,
        //        DatePosted = (DateTime)dto.DatePosted
        //    };

        //    context.Posts.Add(post);
        //    context.SaveChanges();
        //    //OutgoingPostDto dto1 = GetOutgoingPost(post.PostID);
        //    OutgoingPostDto outDto = new OutgoingPostDto()
        //    {
        //        UserId = post.UserId,
        //        PostImageBase64 = post.ImageName,
        //        Text = post.Text,
        //        DatePosted = post.DatePosted
        //    };
        //    return outDto;
        //}
    }
}