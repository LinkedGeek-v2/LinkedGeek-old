using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GroupProject.DAL;
using GroupProject.Models.SharedModels;

namespace GroupProject.HubModels
{
    public class NotificationService
    {
        public IEnumerable<Notification> GetNotifications(string userId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Notifications
                    .Where(n => n.OtherUserId == userId && DbFunctions.DiffDays(DateTime.Now, n.DateTimeSent) <= 31) //(DateTime.Now - n.DateTimeSent).Days <= 31
                    .OrderByDescending(n => n.DateTimeSent)
                    .ToList();
            }
        }

        public void PostNotification(Notification notification)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Notifications.Add(notification);
                db.SaveChanges();
            }
        }
    }
}