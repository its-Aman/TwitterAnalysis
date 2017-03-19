using System;
using System.Collections.Generic;
using System.Linq;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace T001.Models
    {
    public class GetMostDetails
        {
        private IEnumerable<ITweet> temp;

        public List<_MostDetails> _GetMostRetweetedTweets;
        public List<_MostDetails> _GetMostFavouriteTweets;

        public GetMostDetails(IUser user, DateTime fromDate, DateTime toDate)
            {
            temp = GetTweets.GetTweetsForKeyowrds(user);

            _GetMostRetweetedTweets = new List<_MostDetails>();
            _GetMostFavouriteTweets = new List<_MostDetails>();

            GetMostRetweetedTweets(fromDate, toDate);
            GetMostFavouriteTweets(fromDate, toDate);
            }

        public void GetMostRetweetedTweets(DateTime FromDate, DateTime ToDate)
            {
            if (temp != null)
                {
                temp.Where(x => x.RetweetCount > 0 && ToDate.Date >= x.CreatedAt.Date && x.CreatedAt.Date >= FromDate.Date)
                    .OrderByDescending(x => x.RetweetCount).Take(50).ToList()
                    .ForEach(
                    x => _GetMostRetweetedTweets.Add(
                        new _MostDetails
                            {
                            Favourite = x.FavoriteCount,
                            Retweet = x.RetweetCount,
                            TweetDate = x.CreatedAt,
                            TweetText = x.Text,
                            TwitteHandle = x.CreatedBy.ScreenName,
                            UserName = x.CreatedBy.Name,
                            ProfileIcon = x.CreatedBy.ProfileImageUrl,
                            url = x.Url
                            }));
                }
            }

        public void GetMostFavouriteTweets(DateTime FromDate, DateTime ToDate)
            {
            if (temp != null)
                {
                temp.Where(x => x.FavoriteCount > 0 && ToDate.Date >= x.CreatedAt.Date && x.CreatedAt.Date >= FromDate.Date)
                    .OrderByDescending(x => x.FavoriteCount).Take(50).ToList()
                    .ForEach(
                    x => _GetMostFavouriteTweets.Add(
                        new _MostDetails
                            {
                            Favourite = x.FavoriteCount,
                            Retweet = x.RetweetCount,
                            TweetDate = x.CreatedAt,
                            TweetText = x.Text,
                            TwitteHandle = x.CreatedBy.ScreenName,
                            UserName = x.CreatedBy.Name,
                            ProfileIcon = x.CreatedBy.ProfileImageUrl,
                            url = x.Url
                            }));
                }
            }
        }
    }