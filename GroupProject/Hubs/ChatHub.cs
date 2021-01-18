using GroupProject.HubModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;

namespace GroupProject.Hubs
{
    [Authorize]
    [HubName("chat")]
    public class ChatHub : Hub
    {
        private readonly UnitOfWork _unitOfWork = UnitOfWork.Access();
        private string CurrentUserID => Context.User.Identity.GetUserId();
        private string CurrentUserConnectionID => Context.ConnectionId;
        private string CurrentUserName => Context.User.Identity.Name;


        public override Task OnConnected()
        {
            _unitOfWork.Connect(CurrentUserID, CurrentUserName, CurrentUserConnectionID);
            return base.OnConnected();
        }

        public void SendMsg(string receiverID, string message)
        {

            if (message.Contains("<script>"))
            {
                //do nothing 
            }
            else if(!string.IsNullOrEmpty(message))
            {
                var messageObj = Message.CreateMessage(CurrentUserName, message);

                _unitOfWork.SaveMessageToHistory(CurrentUserID, receiverID, messageObj);

                var messageJson = _unitOfWork.ConvertMessageToJson(messageObj);

                var toWhom = _unitOfWork.ToWhom(receiverID);

                //if toWhom is null means user is not active

                if (toWhom != null)
                {
                    Clients.Client(toWhom.ConnectionID).receive(messageJson);
                }
            }
        }

        public void GetSelectedUserChatHistory(string withWhoId, int timesRequested)
        {
            var historyJson = _unitOfWork.GetListOfMessagesJson(CurrentUserID, withWhoId,timesRequested);
            Clients.Client(CurrentUserConnectionID).getHistory(historyJson);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            _unitOfWork.Disconnect(CurrentUserID);
            return base.OnDisconnected(stopCalled);
        }

        public void CheckUserStatus(string whoID)
        {
            bool IsActive = false;
            var user = _unitOfWork.UserStatus(whoID);
            if (user != null) IsActive = true;
            Clients.Client(CurrentUserConnectionID).userStatus(IsActive);
        }
    }
}