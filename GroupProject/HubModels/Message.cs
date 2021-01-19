using System;
using System.Collections.Generic;
using System.IO;

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

        public static List<Message> GetMessages(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);

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

            return messages;
           
        }

    }
}