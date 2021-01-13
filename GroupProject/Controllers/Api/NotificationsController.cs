using System.Web.Http;
using GroupProject.HubModels;
using Microsoft.AspNet.Identity;

namespace GroupProject.Controllers.Api
{
    [RoutePrefix("api/notifications")]
    public class NotificationsController : ApiController
    {
        private string userId => User.Identity.GetUserId();

        [HttpGet]
        public IHttpActionResult Notifications()
        {
            var service = new NotificationService();

            return Ok(service.GetNotifications(userId));
        }
    }
}