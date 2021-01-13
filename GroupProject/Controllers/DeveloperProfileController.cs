using AutoMapper;
using GroupProject.DAL;
using GroupProject.Models.DeveloperModels;
using GroupProject.Repositories;
using GroupProject.ViewModels;
using GroupProject.ViewModels.DeveloperViewModels.ProfilePageViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GroupProject.Controllers
{
    public class DeveloperProfileController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly CompanyRepository _companyRepository;
        private readonly DeveloperRepository _developerRepository;

        public DeveloperProfileController()
        {
            _db = new ApplicationDbContext();
            _companyRepository = new CompanyRepository(_db);
            _developerRepository = new DeveloperRepository(_db);
        }
            
        public ActionResult DeveloperProfilePage()
        {
            var Id = User.Identity.GetUserId();

            var developer = _developerRepository.GetDeveloperForProfilePageWithID(Id);

            var devViewModel = Mapper.Map<Developer, DeveloperProfilePageViewModel>(developer);

            devViewModel.SortExperiencesWithNullsFirst();
            devViewModel.SortEducationsWithNullsFirst();

            return View(devViewModel);
        }

        public ActionResult AddressForm(AddressProfilePageViewModel a)  
        {
            //Clears validation messages on first load
            ModelState.Clear();
           
            return PartialView(a);
        }

        public ActionResult DetailsForm(DeveloperDetailsViewModel det)
        {
            ModelState.Clear();
            return PartialView(det);
        }

        public ActionResult EducationForm(EducationProfilePageViewModel e)
        {
            ModelState.Clear();
            return PartialView(e);
        }

        public ActionResult ExperienceForm(ExperienceProfilePageViewModel ex)
        {
            ModelState.Clear();
            ex.CompaniesToChoose = _companyRepository.GetCompaniesNamesAndIDs();
           

            return PartialView(ex);
        }

        public ActionResult SkillForm()
        {
            var skills = _db.Skills.ToList();

            return PartialView(skills);
        }



        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }

    }

   
}