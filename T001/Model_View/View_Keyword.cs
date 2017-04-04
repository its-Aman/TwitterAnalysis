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
    public class View_Keyword
        {
        IUser user;

        public int noOfTweets;
        public int userTweeted;
        public string keyword;

        public KeywordTweetsDetails mostDetails;
        public KeywordUserDetails userDetails;
        public GetLocation location;
        public GetUserMostHashtags hashtags;



        public View_Keyword(IUser user, string keyword)
            {
            this.user = user;
            this.keyword = keyword;
            mostDetails = new KeywordTweetsDetails(keyword);
            userDetails = new KeywordUserDetails(keyword);
            location = new GetLocation();
            hashtags = new GetUserMostHashtags(this.user);
            }

        //graph
        Dictionary<List<string>, IEnumerable<ComplexDataset>> LineGraph_NoOfTweetsForKeywords(IEnumerable<ITweet> query)
            {
            Dictionary<List<string>, IEnumerable<ComplexDataset>> dt = new Dictionary<List<string>, IEnumerable<ComplexDataset>>();

            //todo: Some Serious Stuff
            return dt;
            }

        /*
         mostDetails.GetRecentTweets();
         mostDetails.GetMostRetweetedTweets();
         mostDetails.GetMostFavouriteTweets();
         */

        /*
         UserTweeted;
         userDetails.GetMostFollowedUsers();
         userDetails.GetUsersUsedTheKeywordMost();  
       */
        //location.MapLocation

        //location.LocationOfUSersUsedThatKeyword;

        //hashtag.getHashtags()
        }
    }