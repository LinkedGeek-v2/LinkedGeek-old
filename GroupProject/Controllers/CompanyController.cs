using AutoMapper;
using GroupProject.DAL;
using GroupProject.Models.CompanyModels;
using GroupProject.Persistence;
using GroupProject.Repositories;
using GroupProject.ViewModels.CompanyViewModels;
using System;
using System.Net;
using System.Web.Mvc;

namespace GroupProject.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly CompanyRepository companyRepository;
        private readonly UnitOfWork unitOfWork;

        public CompanyController()
        {
            db = new ApplicationDbContext();
            companyRepository = new CompanyRepository(db);
            unitOfWork = new UnitOfWork(db);
        }


        public ActionResult CreateCompany(string userId)
        {
            var viewModel = new CompanyFormViewModel(userId);

            return View("CompanyForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCompany(CompanyFormViewModel viewModel)
        {
            if (!ModelState.IsValid) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var company = Mapper.Map<CompanyFormViewModel, Company>(viewModel);
            try
            {
                companyRepository.AddCompany(company);

                unitOfWork.Save();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return View("CompanyForm");
            }
        }
       
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
    }
}