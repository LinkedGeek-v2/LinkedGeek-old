using System.IO;

namespace GroupProject.HubModels
{
    public sealed class ConnectedUser
    {
        public string UserID { get; private set; }
        public string UserName { get; set; }
        public string ConnectionID { get;set; }

        private ConnectedUser() { }

        public ConnectedUser(string userId,string userName, string connectionID)
        {
            UserID = userId;
            UserName = userName;
            ConnectionID = connectionID;
        }

        public static ConnectedUser CreateConnectedUser(string UserID,string UserName, string ConnectionID) =>  new ConnectedUser(UserID,UserName,ConnectionID);

        public void SaveMessage(string receiverID,Message message)
        {
            var folderPath = FindPath.FindOrCreateFolderPath(UserID, receiverID);
            var filePath = FindPath.FindOrCreateFilePath(folderPath,message.TimeSent);

            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                var text = $"{UserName}\r\n{message.MessageContent}\r\n{message.TimeSent.ToString("HH:mm")}";
                sw.WriteLine(text);
            };
        }
    }
}