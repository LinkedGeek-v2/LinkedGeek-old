using GroupProject.ApiModels.DeveloperDTOs;
using GroupProject.DAL;
using GroupProject.Persistence;
using GroupProject.Repositories;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GroupProject.Controllers.Api
{
    [RoutePrefix("api/jobs")]
    public class JobsController : ApiController
    {
        private readonly ApplicationDbContext db;
        private readonly JobRepository jobRepository;
        private readonly JobsAppliedRepository jobsAppliedRepository;
        private readonly UnitOfWork unitOfWork;
        private string userId => User.Identity.GetUserId();

        public JobsController()
        {
            db = new ApplicationDbContext();
            jobRepository = new JobRepository(db);
            jobsAppliedRepository = new JobsAppliedRepository(db);
            unitOfWork = new UnitOfWork(db);
        }


        [HttpPost]
        [Route("search")]
        public IHttpActionResult JobsPage(JobPostDto search)
        {
            if (!ModelState.IsValid)
                return BadRequest("Something went wrong!");

            var jobs = jobRepository.GetDevJobs(search, userId);

            return Ok(jobs);
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult JobsPage(int id)
        {
            jobsAppliedRepository.AddJobApplied(userId, id);

            unitOfWork.Save();
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
    }
}
