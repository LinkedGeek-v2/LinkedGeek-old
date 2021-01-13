using AutoMapper;
using GroupProject.DAL;
using GroupProject.Models;
using GroupProject.Models.CompanyModels;
using GroupProject.Repositories;
using GroupProject.ViewModels.CompanyViewModels;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Web.Mvc;

namespace GroupProject.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //if (!User.Identity.IsDeveloper())
            //    return new DeveloperProfileController().DeveloperProfilePage();
            //else
            //    return new CompanyProfilePageController().CompProfilePage();

            //if(!User.Identity.IsDeveloper())
            //{

            //    var company = new CompanyRepository(context).GetCompany(userId);
            //    if (company == null)
            //        return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            //    return View("../Company/ProfilePage/CompProfilePage", Mapper.Map<Company, CompanyFormViewModel>(company));
            //}
            //else
            //{


            //}

            return View();
           
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Chat()
        {
            return View();
        }
    }
}
