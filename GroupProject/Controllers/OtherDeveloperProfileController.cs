using AutoMapper;
using GroupProject.DAL;
using GroupProject.Models.DeveloperModels;
using GroupProject.Repositories;
using GroupProject.ViewModels.DeveloperViewModels.ProfilePageViewModels;
using System.Web.Mvc;

namespace GroupProject.Controllers
{
    [AllowAnonymous]
    public class OtherDeveloperProfileController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly CompanyRepository _companyRepository;
        private readonly DeveloperRepository _developerRepository;

        public OtherDeveloperProfileController()
        {
            _db = new ApplicationDbContext();
            _companyRepository = new CompanyRepository(_db);
            _developerRepository = new DeveloperRepository(_db);
        }

        public ActionResult OtherDeveloperProfilePage(string id)
        {
            var developer = _developerRepository.GetDeveloperForProfilePageWithID(id);
            var devViewModel = Mapper.Map<Developer, DeveloperProfilePageViewModel>(developer);

            devViewModel.SortExperiencesWithNullsFirst();
            devViewModel.SortEducationsWithNullsFirst();
            return View(devViewModel);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }
    }
}