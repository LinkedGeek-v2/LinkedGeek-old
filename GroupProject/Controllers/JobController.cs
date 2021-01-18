using GroupProject.DAL;
using GroupProject.Persistence;
using GroupProject.Repositories;
using GroupProject.ViewModels.CompanyViewModels;
using System.Net;
using System.Web.Mvc;

namespace GroupProject.Controllers
{
    [Authorize]
    public class JobController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly JobRepository jobRepository;
        private readonly UnitOfWork unitOfWork;

        public JobController()
        {
            db = new ApplicationDbContext();
            jobRepository = new JobRepository(db);
            unitOfWork = new UnitOfWork(db);
        }


        public ActionResult DevJobsPage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateJob(JobPostViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            jobRepository.AddJobPosted(viewModel);

            unitOfWork.Save();
            return new HttpStatusCodeResult(HttpStatusCode.OK); //return Json();
        }

        [HttpDelete]
        public ActionResult DeleteJob(int id)
        {
            jobRepository.DeleteJob(id);

            unitOfWork.Save();
            return new HttpStatusCodeResult(HttpStatusCode.OK); //return Json();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
    }
}