using System;

namespace GroupProject.HubModels
{
    public class Message
    {
        public string FromUserName { get; set; }
        public string MessageContent{ get; set; }
        public DateTime TimeSent { get; set;}

        public Message() { }

        public Message(string senderUserName, string messageContent)
        {
            FromUserName = senderUserName;
            MessageContent = messageContent;
            TimeSent = DateTime.Now;
        }

        public static Message CreateMessage(string senderUserName, string message) => new Message(senderUserName, message);
    }
}