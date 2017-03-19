using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tweetinvi;
using Tweetinvi.Logic;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace T001.Models
    {
    public class UserStats
        {
        public string UserScreenName;
        public int UserFollowers;
        public int UserFollowing;
        public int UserListed;
        public decimal Ratio_Follower_Following = 0;
        public decimal Ratio_Listed_100Followers = 0;

        public decimal AverageTweetsPerDay = 0;
        public int UserMentionsWithinTweets = 0;
        public decimal Ratio_UserMentions_Tweets_Ratio = 0;
        public int LinksInTweets = 0;
        public decimal Ratio_Link_Tweet = 0;
        public int TweetsAreRetweets = 0;
        public int TweetsAreReplies = 0;
        public int TweetRetweetByOther = 0;
        public int TweetFavByOther = 0;
        public int TimesTheTweetsWereRetweeted = 0;

        private DateTime FromDate;
        private DateTime ToDate;

        public UserStats(IUser user, DateTime fromDate, DateTime toDate)
            {
            this.FromDate = fromDate;
            this.ToDate = toDate;

            CalculateUserStats(user);
            }

        private void CalculateUserStats(IUser user)
            {
            var temp = Search.SearchTweets(new SearchTweetsParameters(user.ScreenName)
                {
                Since = FromDate,
                Until = ToDate
                });

            UserScreenName = user.Name;
            UserFollowers = user.FollowersCount;
            UserFollowing = user.FriendsCount;
            UserListed = user.ListedCount;

            Ratio_Follower_Following = (UserFollowing > 0) ? UserFollowers / Convert.ToDecimal(UserFollowing) : 0;
            Ratio_Listed_100Followers = (UserFollowers > 0) ? UserListed / Convert.ToDecimal(UserFollowers) : 0;


            AverageTweetsPerDay = Convert.ToDecimal(temp.Count() / ((ToDate - FromDate).TotalDays));

            foreach (var item in temp)
                {
                UserMentionsWithinTweets += item.UserMentions.Count;
                TimesTheTweetsWereRetweeted += item.RetweetCount;
                TweetFavByOther += item.FavoriteCount;
                TweetRetweetByOther += (item.IsRetweet) ? 1 : 0;
                }

            if (temp.Count() > 0)
                {
                Ratio_UserMentions_Tweets_Ratio = UserMentionsWithinTweets / Convert.ToDecimal(temp.Count());
                Ratio_Link_Tweet = LinksInTweets / Convert.ToDecimal(temp.Count());
                }

            LinksInTweets = temp.Where(x => x.Entities.Urls.Count > 0).Count();
            TweetsAreRetweets = temp.Where(x => x.CreatedBy.ScreenName == user.ScreenName && x.IsRetweet).Count();
            TweetsAreReplies = temp.Where(x => x.CreatedBy.ScreenName == user.ScreenName && x.InReplyToScreenName != null).Count();
            }

        }
    }