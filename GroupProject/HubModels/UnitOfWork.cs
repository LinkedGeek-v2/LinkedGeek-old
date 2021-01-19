using GroupProject.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace GroupProject.HubModels
{
    public sealed class UnitOfWork
    {
        private static List<ConnectedUser> ConnectedUsers;                                 //readonly???
        private static UnitOfWork _unitOfWork;

        static UnitOfWork()
        {
            ConnectedUsers = new List<ConnectedUser>();
        }


        private static object syncLock = new object();

        public static UnitOfWork Access()
        {
            if (_unitOfWork == null)
            {
                lock (syncLock)
                {
                    if (_unitOfWork == null)
                    {
                        _unitOfWork = new UnitOfWork();
                    }
                }
            }
            return _unitOfWork;
        }

        public void SaveMessageToHistory(string senderID, string receiverID, Message message)
        {
            var conUser = ConnectedUsers.SingleOrDefault(con => con.UserID == senderID);
            //here user cant be null because he is connected. But i must defend just in case    TODO list
            conUser.SaveMessage(receiverID, message);
        }

        public void Disconnect(string UserId) => ConnectedUsers.RemoveAll(con => con.UserID == UserId);     //Dont know if this is optimized

        public void Connect(string UserID, string UserName, string UserConnectionID)
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

        public string ConvertMessageToJson(Message message)
        {
            return JsonConvert.SerializeObject(message, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        public string GetListOfMessagesJson(string who, string withWhom, int timesRequested)
        {
            var folderPath = FindPath.FindOrCreateFolderPath(who, withWhom);
            var filePaths = FindPath.GetTxtFileNamesOfFolder(folderPath).ToList();

            //if count is 0 it means users havent chatted at all and  it should return a different message to do
            if (filePaths.Count == 0 || timesRequested > filePaths.Count - 1 )
            {
                return "You got all Chat History!";
            }

            //SORT LIST BASED ON NAME OF TXT FILE
            var orderedFilePaths = filePaths.OrderBy(fp => fp , new FileNamesOrderByDate<string>()).ToList();

            //Get Requested History
            var dayHistoryRequested = orderedFilePaths[orderedFilePaths.Count - 1 - timesRequested];

            //Get Messages In File
            var messages = Message.GetMessages(dayHistoryRequested);

            //Convert them to JSON
            var json = JsonConvert.SerializeObject(messages, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            return json;
        }

        public ConnectedUser ToWhom(string toWhoUserID)
        {
            return ConnectedUsers.SingleOrDefault(cu => cu.UserID == toWhoUserID);

        }

        public ConnectedUser UserStatus(string userID)
        {
            return ConnectedUsers.SingleOrDefault(con => con.UserID == userID);
        }
    }
}