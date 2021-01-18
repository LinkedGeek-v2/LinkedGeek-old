using GroupProject.DAL;
using GroupProject.Models.AssociativeModels;
using GroupProject.Persistence;
using GroupProject.Repositories;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GroupProject.Controllers.Api
{
    [Authorize]
    [RoutePrefix("developerSkills")]
    public class DeveloperSkillsPostController : ApiController
    {
        private readonly ApplicationDbContext db;
        private readonly DeveloperSkillsRepository _developerSkillsRepository;
        private readonly SkillRepository _skillRepository;
        private readonly UnitOfWork _unitOfWork;


        public DeveloperSkillsPostController()
        {
            db = new ApplicationDbContext();
            _developerSkillsRepository = new DeveloperSkillsRepository(db);
            _unitOfWork = new UnitOfWork(db);
            _skillRepository = new SkillRepository(db);
        }


        [Route("add/{id:int}")]
        [HttpPost]
        public IHttpActionResult AddSkill(int id)
        {
            if(id ==0) return BadRequest("You must choose a skill!");

            var userID = User.Identity.GetUserId();

            if (_developerSkillsRepository.ExistInDB(userID, id)) return BadRequest("You already have this skill!");

            var developerSkill = DeveloperSkills.Create(id, userID);
            _developerSkillsRepository.Add(developerSkill);

            _unitOfWork.Save();

            return Ok();
        }

        [Route("skills")]
        [HttpGet]
        public IHttpActionResult GetSkills()
        {
            var skills = _skillRepository.GetSkills();

            return Ok(skills);
        }

        [Route("delete/{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteSkill(int id)
        {
            var userId = User.Identity.GetUserId();
            var developerSkill = DeveloperSkills.Create(id,userId);
            _developerSkillsRepository.Delete(developerSkill);

            _unitOfWork.Save();

            return Ok();

        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
    }
}