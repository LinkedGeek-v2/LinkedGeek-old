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
    public class CompanyProfilePageController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly CompanyRepository companyRepository;
        private readonly JobsAppliedRepository jobsAppliedRepository;
        private readonly JobRepository jobRepository;
        private readonly UserRepository userRepository;
        private string userId => User.Identity.GetUserId();

        public CompanyProfilePageController()
        {
            db = new ApplicationDbContext();
            companyRepository = new CompanyRepository(db);
            jobsAppliedRepository = new JobsAppliedRepository(db);
            jobRepository = new JobRepository(db);
            userRepository = new UserRepository(db);
        }


        public ActionResult CompProfilePage()
        {
            var company = companyRepository.GetCompany(userId);
            if (company == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View("../Company/ProfilePage/CompProfilePage", Mapper.Map<Company, CompanyFormViewModel>(company));
        }

        public ActionResult Home()
        {
            var user = db.Users.Include(u => u.Company).Include(u => u.Developer).SingleOrDefault(u => u.Id == userId);
            var photo = user.GetUserPhotoPath();

            var view = new UserViewModel()
            {
                UserId = userId,
                Name = user.IsDeveloper ? user.Developer.FullName : user.Company.CompanyName,
                UserImageUrl = user.GetUserPhotoPath(),
            };

            return PartialView("../User/HomePage", view);
        }

        public ActionResult About()
        {
            var company = companyRepository.GetCompany(userId);
            if (company == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return PartialView("../Company/ProfilePage/About", company);
        }

        [HttpGet]
        public ActionResult Jobs()
        {
            var jobs = jobRepository.GetCompJobs(userId);
            if (jobs == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            var viewModel = new JobGetViewModel(jobs);

            return PartialView("../Company/ProfilePage/Jobs", viewModel);
        }

        [HttpGet]
        public ActionResult People()
        {
            var workers = userRepository.GetWorkers(userId);
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

            return PartialView("../Company/ProfilePage/People", workersViewModel);
        }


        public ActionResult AddressForm()
        {


            return PartialView();
        }


        public ActionResult DetailsForm()
        {
            var company = companyRepository.VanillaCompany(userId);

            CompanyDetailsViewModel a = new CompanyDetailsViewModel();
            a.CompanyName = company.CompanyName;
            a.FoundationDate = company.FoundationDate;
            a.FounderName = company.FounderName;
            a.Description = company.Description;

            return PartialView(a);
        }

        public ActionResult Applicants()
        {
            var applicants = jobsAppliedRepository.GetApplicants(userId);
            if (applicants == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return PartialView("../Company/ProfilePage/Applicants", applicants);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
    }
}