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
    public class View_Profile
        {

        public GetMostDetails mostDetails;
        public GetUserDetails userDetails;
        public UserStats userStats;
        public GetUserMostHashtags hashtags;
        public ProfileGraph profileGraph;

        public static string HandleName;
        public static DateTime FromDate;
        public static DateTime ToDate;
        public static string Tweets_Date;


        public View_Profile(IUser user)
            {
            HandleName = user.ScreenName;
            ToDate = DateTime.Today;
            FromDate = DateTime.Today.AddDays(-14);

            mostDetails = new GetMostDetails(user, FromDate, ToDate);
            userDetails = new GetUserDetails(user, FromDate, ToDate);
            userStats = new UserStats(user, FromDate, ToDate);
            hashtags = new GetUserMostHashtags(user, FromDate, ToDate);
            profileGraph = new ProfileGraph(user,FromDate,ToDate);
            Tweets_Date = "tweets from " + FromDate.Date.ToString() + " to " + ToDate.Date.ToString();
            }

        //Methods

        //Chart.Mvc Complex BarChart
        //profileGraph._LineGraph_NoOfTweetsByDate;

        //userDetails.GetUserMostRetweet
        //userDetails.GetUserMostRepliesTo
        //userDetails.GetUserMostMention

        //Top Hash tags

        //profileGraph._BarGraph_NoOfTweetsByDayOfWeek;

        //profileGraph._BarGraph_NoOfTweetsByHour;

        }
    }