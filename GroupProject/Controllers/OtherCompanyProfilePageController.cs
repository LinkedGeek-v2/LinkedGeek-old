using AutoMapper;
using GroupProject.DAL;
using GroupProject.Models.CompanyModels;
using GroupProject.Repositories;
using GroupProject.ViewModels;
using GroupProject.ViewModels.CompanyViewModels;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace GroupProject.Controllers
{
    [AllowAnonymous]
    public class OtherCompanyProfilePageController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly CompanyRepository companyRepository;
        private readonly JobRepository jobRepository;
        private readonly JobsAppliedRepository jobsAppliedRepository;
        private readonly UserRepository userRepository;

        public OtherCompanyProfilePageController()
        {
            db = new ApplicationDbContext();
            companyRepository = new CompanyRepository(db);
            jobRepository = new JobRepository(db);
            jobsAppliedRepository = new JobsAppliedRepository(db);
            userRepository = new UserRepository(db);
        }


        public ActionResult OtherCompProfilePage(string id)
        {
            //id = "0d753708-3172-4ea1-8aeb-9bea394f5f3a";
            var viewModel = Mapper.Map<Company, CompanyFormViewModel>(companyRepository.GetCompany(id));

            return View("../Company/OtherProfilePage/OtherCompProfilePage", viewModel);
        }

        public ActionResult Home(string companyId)
        {
            if (companyId == null)
                companyId = User.Identity.GetUserId();

            var user = db.Users.Include(u => u.Company).Include(u => u.Developer).SingleOrDefault(u => u.Id == companyId);
            var photo = user.GetUserPhotoPath();

            var view = new UserViewModel()
            {
                UserId = companyId,
                Name = user.IsDeveloper ? user.Developer.FullName : user.Company.CompanyName,
                UserImageUrl = user.GetUserPhotoPath(),
            };

            return PartialView("../User/OtherHomePage",view);
        }

        public ActionResult About(string companyId)
        {
            var company = companyRepository.GetCompany(companyId);
            if (company == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return PartialView("../Company/OtherProfilePage/_About", company);
        }

        public ActionResult Jobs(string companyId)
        {
            var jobs = jobRepository.GetCompJobs(companyId);
            if (jobs == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            var viewModel = new JobGetViewModel(jobs);

            return PartialView("../Company/OtherProfilePage/_Jobs", viewModel);
        }

        [HttpGet]
        public ActionResult People(string companyId)
        {
            var workers = userRepository.GetWorkers(companyId);
            var workersViewModel = new List<CompanyPeopleViewModel>();

            foreach (var worker in workers)
            {
                workersViewModel.Add(new CompanyPeopleViewModel
                {
                    ImageName = worker.GetUserPhotoPath(),
                    FullName = worker.Developer.FullName,
                    JobTitle = worker.Developer.Experiences.FirstOrDefault().JobTitle,
                    DeveloperID=worker.Id
                });
            }

            return PartialView("../Company/OtherProfilePage/_People", workersViewModel);
        }

        public ActionResult Applicants(string companyId)
        {
            var applicants = jobsAppliedRepository.GetApplicants(companyId);
            if (applicants == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return PartialView("../Company/OtherProfilePage/_Applicants", applicants);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
    }
}