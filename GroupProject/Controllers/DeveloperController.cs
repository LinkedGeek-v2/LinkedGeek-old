using AutoMapper;
using GroupProject.DAL;
using GroupProject.Models.DeveloperModels;
using GroupProject.Persistence;
using GroupProject.Repositories;
using GroupProject.ViewModels;
using System;
using System.Net;
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

        public ActionResult CreateDeveloper(string userId)
        {
            var viewModel = new DeveloperFormViewModel(userId);

            return View("DeveloperForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDeveloper(DeveloperFormViewModel viewModel)
        {
            if (!ModelState.IsValid) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var developer = Mapper.Map<DeveloperFormViewModel, Developer>(viewModel);
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