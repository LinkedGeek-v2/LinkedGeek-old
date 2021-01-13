using GroupProject.DAL;
using GroupProject.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GroupProject.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext context;

        public UserController()
        {
            context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }


        public ActionResult Index()
        {
            return View();
        }

        public string UserPhotos(string Id)
        {
            //If no Id is given then get my photo
            var userId = Id ?? User.Identity.GetUserId();

            var user = context.Users.Include(u => u.Developer).FirstOrDefault(d => d.Id == userId);
            var photopath = user.GetUserPhotoPath();
            return user.GetUserPhotoPath();
        }

        public ActionResult NetworkPage()
        {
            return View();
        }

        public ActionResult HomePage()
        {
            var id = User.Identity.GetUserId();
            var user = context.Users.Include(u => u.Company).Include(u => u.Developer).SingleOrDefault(u => u.Id == id);
            var photo = user.GetUserPhotoPath();

            var view = new UserViewModel()
            {
                UserId = id,
                Name = user.IsDeveloper ? user.Developer.FullName : user.Company.CompanyName,
                UserImageUrl = user.GetUserPhotoPath(),
            };

            return View(view);
        }
    }
}