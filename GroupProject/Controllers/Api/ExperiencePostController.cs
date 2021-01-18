using GroupProject.ApiModels.Incoming.ProfilePage;
using GroupProject.DAL;
using GroupProject.Enums;
using GroupProject.Models.DeveloperModels;
using GroupProject.Persistence;
using GroupProject.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Http;

namespace GroupProject.Controllers.Api
{
    [Authorize]
    [RoutePrefix("experiences")]
    public class ExperiencePostController : ApiController
    {
        private readonly ApplicationDbContext db;
        private readonly ExperienceRepository _experienceRepository;
        private readonly UnitOfWork _unitOfWork;


        public ExperiencePostController()
        {
            db = new ApplicationDbContext();
            _experienceRepository = new ExperienceRepository(db);
            _unitOfWork = new UnitOfWork(db);
        }


        [Route("add-edit")]
        [HttpPost]
        public IHttpActionResult AddEditExperience(ExperiencePostDto experiencePostDto)
        {
            var userId = User.Identity.GetUserId();
            if (!ModelState.IsValid)
            {
                var a = ModelState.Values.SelectMany(msE => msE.Errors).Select(err => err.ErrorMessage);
                var c = ModelState.Keys;  //all keys or all keys that are wrong?  
                var bf = "";
                foreach (var item in a)
                {
                    bf += item + ",";
                }
                return BadRequest(bf);
            }

            var experience = Experience.Create(experiencePostDto, userId);
            _experienceRepository.AddOrEdit(experience);

            _unitOfWork.Save();

            var id = experience.ExperienceID;

            return Ok(id);
        }

        [Route("delete/{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteExperience(int id)
        {
            var experience = Experience.Delete(id);
            _experienceRepository.Delete(experience);

            _unitOfWork.Save();

            return Ok();
        }


        [Route("workingTypes")]
        [HttpGet]
        public IHttpActionResult GetTypeOfJobs()
        {
            var jobTypes = Enum.GetNames(typeof(WorkingType));

            return Ok(jobTypes);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
    }
}