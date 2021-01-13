using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GroupProject.HubModels
{
    public sealed class UnitOfWork
    {
        private static readonly List<ConnectedUser> ConnectedUsers;                                 //readonly???
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

            conUser.IsActive = true;
        }

        public string ConvertMessageToJson(Message message)
        {
            return JsonConvert.SerializeObject(message, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        public string GetListOfMessagesJson(string who, string withWhom, int timesRequested)
        {
            var folderPath = FindPath.FindOrCreateFolderPath(who, withWhom);
            var filePath = FindPath.GetTxtFileNamesOfFolder(folderPath).ToList();

            if (timesRequested > filePath.Count - 1)
            {
                return "You got all Chat History!";
            }

            var dayHistoryRequested = filePath[filePath.Count - 1 - timesRequested];
            //var day = dayHistoryRequested.Substring(dayHistoryRequested.Length - 14, dayHistoryRequested.Length - 1);


            string[] lines = File.ReadAllLines(dayHistoryRequested);

            List<Message> messages = new List<Message>();

            for (int i = 0; i < lines.Length - 1;)
            {
                Message a = new Message
                {
                    FromUserName = lines[i],
                    MessageContent = lines[i + 1],
                    TimeSent = DateTime.Parse(lines[i + 2])
                };
                messages.Add(a);
                i += 3;
            }


            var json = JsonConvert.SerializeObject(messages, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            //var json2 = JsonConvert.SerializeObject(day, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            return json;
        }

        public ConnectedUser ToWhom(string toWhoUserID)
        {
            return ConnectedUsers.SingleOrDefault(cu => cu.UserID == toWhoUserID);

        }


        public bool UserStatus(string userID)
        {
            return ConnectedUsers.SingleOrDefault(con => con.UserID == userID).IsActive;
        }
    }
}