using AutoMapper;
using GroupProject.DAL;
using GroupProject.Models.DeveloperModels;
using GroupProject.Persistence;
using GroupProject.Repositories;
using GroupProject.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;

namespace GroupProject.Controllers
{
    public class DeveloperController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly DeveloperRepository developerRepository;
        private readonly UnitOfWork unitOfWork;

        public DeveloperController()
        {
            db = new ApplicationDbContext();
            developerRepository = new DeveloperRepository(db);
            unitOfWork = new UnitOfWork(db);
        }

        public ActionResult DevJobsPage()
        {

            return View();
        }



        public ActionResult CreateDeveloper()
        {
            return View("DeveloperForm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDeveloper(DeveloperFormViewModel viewModel)
        {
            var developer = Mapper.Map<DeveloperFormViewModel, Developer>(viewModel);
            developer.DeveloperID = User.Identity.GetUserId();
            try
            {
                developerRepository.AddDeveloper(developer);

                unitOfWork.Save();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return View("DeveloperForm");
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
    }
}
