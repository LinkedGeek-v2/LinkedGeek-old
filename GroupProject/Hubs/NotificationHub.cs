using GroupProject.HubModels;
using GroupProject.Models.SharedModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;

namespace GroupProject.Hubs
{
    [Authorize]
    [HubName("notificationHub")]
    public class NotificationHub : Hub
    {
        private readonly UnitOfWork unitOfWork;
        private readonly NotificationService notificationService;
        private string CurrentUserID => Context.User.Identity.GetUserId();

        public NotificationHub()
        {
            unitOfWork = new UnitOfWork();
            notificationService = new NotificationService();
        }

        public void Notify(string content, string userId)
        {
            var notification = Notification.New(content, userId);
            notificationService.PostNotification(notification);

            var connUser = unitOfWork.ToWhom(notification.OtherUserId);
            Clients.Client(connUser.ConnectionID).notifyUser(notification);
        }


        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }
    }
}