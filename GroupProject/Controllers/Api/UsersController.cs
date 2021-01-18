using AutoMapper;
using GroupProject.ApiModels.ChatDTOs;
using GroupProject.ApiModels.UserDTOs;
using GroupProject.DAL;
using GroupProject.Models;
using GroupProject.Persistence;
using GroupProject.Repositories;
using Microsoft.AspNet.Identity;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GroupProject.Controllers.api
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private readonly ApplicationDbContext context;
        private readonly UserRepository repository;
        private readonly UnitOfWork unitOfWork;

        public UsersController()
        {
            context = new ApplicationDbContext();
            repository = new UserRepository(context);
            unitOfWork = new UnitOfWork(context);
        }

        protected override void Dispose(bool disposing)
        {
            repository.Dispose();
        }


        [HttpGet]
        [Route("chat/follow")]
        public IHttpActionResult ChatFollowers()
        {
            var userId = User.Identity.GetUserId();
            var chatFollowees = context.Followings
                 .Where(f => f.FollowerID == userId)
                 .Select(f => f.Followee)
                 .Where(fwee => fwee.Followees.Any(fee => fee.FolloweeID == userId))
                 .Include(c => c.Company)
                 .Include(c => c.Developer)
                 .ToList();


            var chatters = new List<ChatApplicationUserDto>();

            foreach (var user in chatFollowees)
            {
                var chat1 = Mapper.Map<ApplicationUser, ChatApplicationUserDto>(user);
                chat1.ImageName = user.GetUserPhotoPath();
                chatters.Add(chat1);

            }

            return Ok(chatters);
        }

        [HttpGet]
        [Route("followees/{Id}")]
        public IEnumerable Followees(string id = null)//works without id?
        {
            var UserId = id ?? User.Identity.GetUserId();
            var Followees = repository.Followees(UserId).ToList();
            var UserDtos = MapToDto(Followees);

            return UserDtos;
        }

        [HttpGet]
        [Route("followers/{Id}")]
        public IEnumerable Followers(string id = null)
        {
            var UserId = id ?? User.Identity.GetUserId();
            var Followers = repository.Followees(UserId);
            var dtos = MapToDto(Followers).ToList();

            return dtos;
        }

        [HttpGet]
        [Route("recommended")]
        public IEnumerable Recommended()
        {
            var UserId = User.Identity.GetUserId();
            var recommended=repository.StrongOfStrongFollowees(UserId);
            if (recommended.Count() == 0)
                recommended = repository.AllFolloweesExceptMine(UserId);

            var dtos = MapToDto(recommended);

            return dtos;
        }

        [HttpDelete]
        [Route("unFollow/{Id}")]
        public IHttpActionResult UnFollow(string id)
        {
            var currId = User.Identity.GetUserId();
            if (repository.UnFollow(currId, id))
            {
                unitOfWork.Save();
                return Ok();
            }             
            else
            {
                return NotFound();
            }           
        }

        [HttpPost]
        [Route("follow/{Id}")]
        public IHttpActionResult Follow(string id)
        {
            var currId = User.Identity.GetUserId();
            if (repository.Follow(currId, id))
            {
                unitOfWork.Save();
                return Ok();
            }
            else
            {
                return NotFound();
            }              
        }

        [HttpPost]
        [Route("savepic")]
        public IHttpActionResult SavePic([FromBody] string ImageBase64)
        {
            string id = User.Identity.GetUserId();
            if (ImageBase64 == null)
                return BadRequest();

            repository.SavePic(id, ImageBase64);
            return Ok();
        }

        private IEnumerable<ApplicationUserNetworkDto> MapToDto(IEnumerable<ApplicationUser> Users)
        {
            var UserDtos = new List<ApplicationUserNetworkDto>();
            foreach (var user in Users)
            {
                if (user.IsDeveloper)
                    UserDtos.Add(new ApplicationUserNetworkDto()
                    {
                        Developer = new DeveloperNetworkDto()
                        {
                            DeveloperID = user.Id,
                            FirstName = user.Developer.FirstName,
                            LastName = user.Developer.LastName,
                            FullName = user.Developer.FullName,
                            CompanyWorking = new CompanyNetworkDto()
                            {
                                CompanyID = user.Developer.CompanyWorkingId,
                                CompanyName = user.Developer.CompanyWorking.CompanyName
                            }
                        },
                        ImageBase64 = user.GetUserPhotoPath()

                    });

                else
                    UserDtos.Add(new ApplicationUserNetworkDto()
                    {
                        Company = new CompanyNetworkDto()
                        {
                            CompanyName = user.Company.CompanyName,
                            CompanyID = user.Id
                        },
                        ImageBase64 = user.GetUserPhotoPath()

                    });
            }
            return UserDtos;
        }
    }
}