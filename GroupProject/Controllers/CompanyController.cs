using AutoMapper;
using GroupProject.DAL;
using GroupProject.Models.CompanyModels;
using GroupProject.Persistence;
using GroupProject.Repositories;
using GroupProject.ViewModels.CompanyViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GroupProject.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly CompanyRepository companyRepository;
        private readonly UnitOfWork unitOfWork;

        private string userId => User.Identity.GetUserId();

        public CompanyController()
        {
            db = new ApplicationDbContext();
            companyRepository = new CompanyRepository(db);
            unitOfWork = new UnitOfWork(db);
        }

        public ActionResult CreateCompany()
        {
            return View("CompanyForm");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCompany(CompanyFormViewModel viewModel)
        {
            var company = Mapper.Map<CompanyFormViewModel, Company>(viewModel);
            company.CompanyID = userId;
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
