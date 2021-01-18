using GroupProject.ApiModels.Incoming.ProfilePage;
using GroupProject.DAL;
using GroupProject.Models.DeveloperModels;
using GroupProject.Persistence;
using GroupProject.Repositories;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GroupProject.Controllers.Api
{
    [Authorize]
    [RoutePrefix("educations")]
    public class EducationPostController : ApiController
    {
        private readonly ApplicationDbContext db;
        private readonly EducationRepository educationRepository;
        private readonly UnitOfWork _unitOfWork;
        private string userId => User.Identity.GetUserId();


        public EducationPostController()
        {
            db = new ApplicationDbContext();
            educationRepository = new EducationRepository(db);
            _unitOfWork = new UnitOfWork(db);
        }


        [Route("add-edit")]
        [HttpPost]
        public IHttpActionResult AddEducation(EducationPostDto educationPostDto)
        {

            var userID = userId;
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

            var education = Education.Create(educationPostDto , userId);
            educationRepository.AddOrEdit(education);

            _unitOfWork.Save();

            var id = education.EducationID;

            return Ok(id);
        }

        [Route("delete/{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteEducation(int id)
        {
            var education = Education.Delete(id);
            educationRepository.Delete(education);

            _unitOfWork.Save();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
    }
}