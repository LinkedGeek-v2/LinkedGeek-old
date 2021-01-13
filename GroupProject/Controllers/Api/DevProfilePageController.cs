using AutoMapper;
using GroupProject.ApiModels.DeveloperDtos;
using GroupProject.DAL;
using GroupProject.Models.DeveloperModels;
using GroupProject.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Http;

namespace GroupProject.Controllers.Api
{
                                 //***************** OLD API CONTROLLER FOR OLD PROFILE PAGE - REPLACED BY MVC DeveloperProfileController
    [Authorize]
    [RoutePrefix("devProfile")]
    [Obsolete("This controller has been replaced by MVC controller DeveloperProfileController")]
    public class DevProfilePageController : ApiController
    {

        private readonly ApplicationDbContext _db;
        private readonly DeveloperRepository _developerRepository;
        private readonly CompanyRepository _companyRepository;

        public DevProfilePageController()
        {
            _db = new ApplicationDbContext();
            _developerRepository = new DeveloperRepository(_db);
            _companyRepository = new CompanyRepository(_db);
        }


        //Get ProfilePage
        [Route("show")]
        [HttpGet]
        public IHttpActionResult ShowProfilePage()
        {
            var Id = User.Identity.GetUserId();

            var developer = _developerRepository.GetDeveloperForProfilePageWithID(Id);

            var devDto = Mapper.Map<Developer, DeveloperDto>(developer);

            return Ok(devDto);
        }


        [Route("others")]
        [HttpGet]
        public IHttpActionResult ShowOtherDevProfiles(string id)
        {

            var developer = _developerRepository.GetDeveloperForProfilePageWithID(id);

            if (developer == null) return NotFound();


            var devDto = Mapper.Map<Developer, DeveloperDto>(developer);

            return Ok(devDto);
        }



        [Route("getCompanies")]
        [HttpGet]
        public IHttpActionResult GetCompanies()
        {
            var companies = _companyRepository.GetCompaniesNamesAndIDs();

            return Ok(companies);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }
    }
}
