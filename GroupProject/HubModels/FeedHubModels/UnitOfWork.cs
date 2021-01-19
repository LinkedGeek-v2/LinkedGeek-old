using System.Collections.Generic;
using System.Linq;

namespace GroupProject.HubModels.FeedHubModels
{
    public sealed class UnitOfWork
    {
        private static readonly List<ConnectedUser> ConnectedUsers = new List<ConnectedUser>();                                              //readonly
        //private static readonly List<ConversationHistoryFolder> ConversationHistoryFolders = new List<ConversationHistoryFolder>();          //readonly
        //private static readonly List<ConversationHistoryFolder> ConversationHistoryFolders = new List<ConversationHistoryFolder>();          //readonly

        public void CreateMap(string UserID, string UserName, string UserConnectionID)
        {
            var conUser = ConnectedUsers.SingleOrDefault(cu => cu.UserID == UserID);

            if (conUser == null)
            {
                conUser = ConnectedUser.CreateConnectedUser(UserID, UserName, UserConnectionID);
                ConnectedUsers.Add(conUser);
            }
            else
            {
                conUser.ConnectionID = UserConnectionID;
            }


        }

        //public void SaveHistory(string userID, string toWhomID, Message message)
        //{
        //    var currentUser = ConnectedUsers.SingleOrDefault(cu => cu.UserID == userID);

        //    var chatHistoryFolder = ConversationHistoryFolders.SingleOrDefault(chf => chf.WhoID == userID && chf.WithWhomID == toWhomID);

        //    if (chatHistoryFolder == null)
        //    {
        //        chatHistoryFolder = ConversationHistoryFolder.GetOrCreateConversationHistoryFolder(userID, toWhomID);
        //        ConversationHistoryFolders.Add(chatHistoryFolder);
        //    }

        //    chatHistoryFolder.Save(currentUser.UserName, message);

        //}


        //public string ConvertMessageToJson(Message message)
        //{
        //    return JsonConvert.SerializeObject(message, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        //}


        //public string GetListOfMessagesJson(string who, string withWhom, int timesRequested)
        //{
        //    var history = ConversationHistoryFolders.SingleOrDefault(chf => chf.WhoID == who && chf.WithWhomID == withWhom);

        //    if (history == null)
        //    {
        //        history = ConversationHistoryFolder.GetOrCreateConversationHistoryFolder(who, withWhom);
        //    }

        //    if (timesRequested > history.ChatFileHistories.Count - 1)
        //    {
        //        return "You got all Chat History!";
        //    }

        //    var day = history.ChatFileHistories[history.ChatFileHistories.Count - 1 - timesRequested];

        //    string[] lines = File.ReadAllLines(day.Path);


        //    List<Message> messages = new List<Message>();


        //    for (int i = 0; i < lines.Length - 1;)
        //    {
        //        Message a = new Message();
        //        a.From = lines[i];
        //        a.MessageContent = lines[i + 1];
        //        a.TimeSent = DateTime.Parse(lines[i + 2]);
        //        messages.Add(a);
        //        i += 3;
        //    }

        //    var json = JsonConvert.SerializeObject(messages, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });


        //    return json;


        //}


        //public void ChangeUserStatus(string userId)
        //{
        //    var user = ConnectedUsers.SingleOrDefault(cu => cu.UserID == userId);

        //    user.IsActive = false;
        //    user.LastOnline = DateTime.Now;

        //}


        //public string SendUserStatus(string whoId)
        //{
        //    var user = ConnectedUsers.SingleOrDefault(cu => cu.UserID == whoId);

        //    if (user == null)
        //        user = ConnectedUser.CreateConnectedUser(whoId, "", "");

        //    return JsonConvert.SerializeObject(user, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

        //}



        //public string ToWhoConnectionID(string toWhoUserID)
        //{
        //    string toWhoConId = ConnectedUsers.SingleOrDefault(cu => cu.UserID == toWhoUserID).ConnectionID;
        //    return toWhoConId;
        //}
    }
}