using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupProject.Models.SharedModels
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string Content { get; private set; }
        public DateTime DateTimeSent { get; private set; }
        public bool IsRead { get; set; }

        [ForeignKey("OtherUser")]
        public string OtherUserId { get; private set; }
        public ApplicationUser OtherUser { get; set; }

        private Notification() { }

        public Notification(string content, string otherId)
        {
            Content = content;
            DateTimeSent = DateTime.Now;
            IsRead = false;
            OtherUserId = otherId;
        }

        public static Notification New(string content, string otherId) => new Notification(content, otherId);
    }
}