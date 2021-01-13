using GroupProject.DAL;
using GroupProject.Repositories;
using GroupProject.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupProject.Hubs.FeedHub
{
    [HubName("feed")]
    public class FeedHub : Hub
    {
        UserRepository repository = new UserRepository(new ApplicationDbContext());

        public static Dictionary<string, string> ConnectionMap = new Dictionary<string, string>();

        public static Dictionary<string, string> VisitorsMap = new Dictionary<string, string>();

        private string CurrentUserID => Context.User.Identity.GetUserId();
        private string CurrentUserConnectionID => Context.ConnectionId;
        private string CurrentUserName => Context.User.Identity.Name;



        public void Send(PostViewModel post)
        {
            var onlineFollowees = GetFollowerConnectionStrings();
            var onlineVisitors = GetVisitorConnectionStrings();

            List<string> onlineReceivers = new List<string>();

            onlineReceivers = AddRangeDistinct(onlineReceivers, onlineFollowees);
            onlineReceivers = AddRangeDistinct(onlineReceivers, onlineVisitors);

            var jsonpost = ConvertMessageToJson(post);
            
            Clients.Clients(onlineReceivers).receivePost(jsonpost);
        }

        public List<string> GetFollowerConnectionStrings()
        {
            List<string> followersConnStrings = new List<string>();
            var followers = repository.Followers(Context.User.Identity.GetUserId());

            foreach (var follower in followers)
                if (ConnectionMap.ContainsKey(follower.Id))
                    if (ConnectionMap[follower.Id] != null)
                        followersConnStrings.Add(ConnectionMap[follower.Id]);


            return followersConnStrings;
        }

        public List<string> GetVisitorConnectionStrings()
        {
            List<string> visitorConnectionStrings = new List<string>();

            foreach (KeyValuePair<string, string> entry in VisitorsMap)
                if (entry.Value == CurrentUserID)
                    visitorConnectionStrings.Add(ConnectionMap[entry.Key]);
            return visitorConnectionStrings;
        }

        public List<string> AddRangeDistinct(List<string>list1,List<string>list2)
        {
            List<string> composedList = new List<String>();
            composedList.AddRange(list1);
            foreach (var iter in list2)
                if (!composedList.Contains(iter))
                    composedList.Add(iter);
            return composedList;
        }

        //visiteeId: Id of the user's home page I am visiting
        //the method is used when someone accesses a home page of another user
        public void AddToVisitorMap(string visiteeId)
        {
            if(!VisitorsMap.ContainsKey(CurrentUserID))
                VisitorsMap.Add(CurrentUserID, visiteeId);
        }

        public string ConvertMessageToJson(PostViewModel post)
        {
            return JsonConvert.SerializeObject(post, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }


        public override Task OnConnected()
        {
            if (!ConnectionMap.ContainsKey(CurrentUserID))
                ConnectionMap.Add(CurrentUserID, CurrentUserConnectionID);

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {

            ConnectionMap.Remove(CurrentUserID);
            VisitorsMap.Remove(CurrentUserID);

            return base.OnDisconnected(stopCalled);
        }

        //public static void RemoveValueFromDictionary(Dictionary<string,string> dictionary, string value)
        //{
        //    foreach (KeyValuePair<string, string> entry in dictionary)
        //    {
        //        if (entry.Value == value)
        //        {
        //            dictionary.Remove(entry.Key);
        //            break;
        //        }
        //    }
        //}


        public override Task OnReconnected()
        {
            // Add your own code here.
            // For example: in a chat application, you might have marked the
            // user as offline after a period of inactivity; in that case 
            // mark the user as online again.
            return base.OnReconnected();
        }
    }
}