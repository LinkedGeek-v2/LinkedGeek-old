using GroupProject.ApiModels.CompanyDTOs;
using GroupProject.ApiModels.Incoming.ProfilePage;
using GroupProject.DAL;
using GroupProject.Models.DeveloperModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GroupProject.Controllers.Api
{
    [Authorize]
    [RoutePrefix("details")]
    public class DetailsForBothController : ApiController
    {
        private readonly ApplicationDbContext _db;
        private string userId => User.Identity.GetUserId();


        public DetailsForBothController()
        {
            _db = new ApplicationDbContext();
        }


        [HttpPatch]
        [Route("dev")]
        public IHttpActionResult DevSaveDetails(DeveloperDetailsPostDto details)
        {
            if(!ModelState.IsValid)
            {
                var a = ModelState.Values.SelectMany(msE => msE.Errors).Select(err => err.ErrorMessage);
                var c = ModelState.Keys;  //all keys or all keys that are wrong?
                var bf = "";
                foreach (var item in a)
                {
                    bf += item + ",";
                }
                return BadRequest(bf);
            }


            var d = _db.Developers.SingleOrDefault(x => x.DeveloperID == userId);

            d.FirstName = details.FirstName;
            d.LastName = details.LastName;
            d.DateOfBirth = details.DateOfBirth;
            d.Gender = details.Gender;
            
            _db.SaveChanges();

            var age = d.Age;
            return Ok(age);

        }



        [HttpPatch]
        [Route("comp")]
        public IHttpActionResult CompanySaveDetails(CompanyDetailsPostDto details)
        {
            if (!ModelState.IsValid)
            {
                var a = ModelState.Values.SelectMany(msE => msE.Errors).Select(err => err.ErrorMessage);
                var bc = ModelState.Keys;  //all keys or all keys that are wrong?
                var bf = "";
                foreach (var item in a)
                {
                    bf += item + ",";
                }
                return BadRequest(bf);
            }


            var c = _db.Companies.SingleOrDefault(x => x.CompanyID == userId);

            c.CompanyName = details.CompanyName;
            c.FoundationDate = details.FoundationDate;
            c.FounderName = details.FounderName;
            c.Description = details.Description;

            _db.SaveChanges();

            return Ok();


        }
    }
}
