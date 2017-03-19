using Chart.Mvc.ComplexChart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using T001.Models;
using Tweetinvi.Logic;
using Tweetinvi.Models;

namespace T001.Model_View
    {
    public class View_Connections
        {
        //get data for previous seven days
        GetUserDetails userDetails;
        GetLocation location;

        public static string Mentions;
        public static string AvgMentionsPerDay;
        public static string UserMentionYou;
        public static string YourTweetRetweeted;
        public static string YourTweetRetweetedInTotal;

        private IUser user;

        public View_Connections(IUser user)
            {
            this.user = user;

            location = new GetLocation();
            userDetails = new GetUserDetails(this.user);
            }

        //first graph
        Dictionary<List<string>, IEnumerable<ComplexDataset>> LineGraph_NoOfTweetsInWhichUserIsMentioned(IEnumerable<ITweet> query)
            {
            Dictionary<List<string>, IEnumerable<ComplexDataset>> dt = new Dictionary<List<string>, IEnumerable<ComplexDataset>>();

            //TODO: Some Serious Stuff

            return dt;
            }

        //userDetails.MostFollowedUsersThatMentionedYou
        //userDetails.UsersWhoMentionedYouTheMost

        //location.MapLocation

        //second graph
        Dictionary<List<string>, IEnumerable<ComplexDataset>> BarGraph_NoOfTweetsRetweeted(IEnumerable<ITweet> query)
            {
            Dictionary<List<string>, IEnumerable<ComplexDataset>> dt = new Dictionary<List<string>, IEnumerable<ComplexDataset>>();

            //TODO: Some Serious Stuff

            return dt;
            }
        }
    }