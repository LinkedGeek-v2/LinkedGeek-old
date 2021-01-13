using AutoMapper;
using GroupProject.ApiModels.PostsDTOs;
using GroupProject.DAL;
using GroupProject.Images;
using GroupProject.Models.SharedModels;
using GroupProject.Persistence;
using GroupProject.Repositories;
using GroupProject.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace GroupProject.Controllers.Api
{
    [RoutePrefix("api/posts")]
    public class UserPostsController : ApiController
    {
        private readonly PostsRepository Repository;
        private readonly ApplicationDbContext context;
        private readonly UnitOfWork unitOfWork;

        public UserPostsController()
        {
            context = new ApplicationDbContext();
            Repository = new PostsRepository(context);
            unitOfWork = new UnitOfWork(context);
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        [HttpPost]
        [Route("createpost")]
        public IHttpActionResult CreatePost(IncomingPostDto IncomingPostDto)
        {
            if (ModelState.IsValid)
            {
                Post post = MapToDto(IncomingPostDto);

                var postCreated = Repository.Add(post);

                unitOfWork.Save();

                OutgoingPostDto outcomingPostDto = Mapper.Map<OutgoingPostDto>(post);

                return Ok(outcomingPostDto);
            }
            else
                return BadRequest(ModelState);
        }

        [HttpGet]
        [Route("getuserfolloweesposts")]
        public IEnumerable<PostViewModel> GetUserRelatedPosts()
        {
            var Posts = Repository.GetUserAndFolloweesPosts(User.Identity.GetUserId());
            return MapToViewModels(Posts.OrderByDescending(p => p.DatePosted));
        }

        [HttpGet]
        [Route("getuserfolloweesposts/{startIndex:int}/{endIndex:int}")]
        public IEnumerable<PostViewModel> GetUserRelatedPosts([FromUri] int startIndex, int endIndex)
        {
            var Posts = Repository.GetUserAndFolloweesPosts(User.Identity.GetUserId(), startIndex, endIndex);
            var mapped = MapToViewModels(Posts.OrderByDescending(p => p.DatePosted));
            return MapToViewModels(Posts.OrderByDescending(p => p.DatePosted));
        }

        [HttpGet]
        [Route("getuserposts/{userId}")]
        public IEnumerable<PostViewModel> GetUserPosts(string userId)
        {
            if (userId == null)
                userId = User.Identity.GetUserId();

            var OutgoingPostDtos = Repository.GetUserSpecificPosts(userId);
            return MapToViewModels(OutgoingPostDtos.OrderByDescending(p => p.DatePosted));
        }

        [HttpGet]
        [Route("getuserposts/{startIndex:int}/{endIndex:int}/{userId}")]
        public IEnumerable<PostViewModel> GetUserPosts([FromUri] int startIndex, int endIndex,string userId)
        {
            if (userId == null)
                userId = User.Identity.GetUserId();
            var Posts = Repository.GetUserSpecificPosts(userId, startIndex, endIndex);
            return MapToViewModels(Posts.OrderByDescending(p => p.DatePosted));
        }

        public IEnumerable<PostViewModel> MapToViewModels(IEnumerable<Post> posts)
        {
            var viewModels = new List<PostViewModel>();

            foreach (var post in posts)
            {
                var viewModel = new PostViewModel
                {
                    Post = Mapper.Map<OutgoingPostDto>(post),
                    UserId = post.User.Id,
                    UserImageUrl = post.User.GetUserPhotoPath(),
                    Name = post.User.IsDeveloper ? post.User.Developer.FullName : post.User.Company.CompanyName,
                    IsDeveloper = post.User.IsDeveloper
                };

                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        public Post MapToDto(IncomingPostDto dto)
        {
            string Id = User.Identity.GetUserId();

            string ImgName = null;
            if (dto.ImageBase64 != null)
                ImgName = ImageHelper.SavePostImg(dto.ImageBase64);

            Post post = new Post()
            {
                UserId = Id,
                DatePosted = DateTime.Now,
                ImageName = ImgName,
                Text = dto.Text
            };
            return post;
        }


    }
}
