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
    public class KeywordTweetsDetails
        {
        IEnumerable<ITweet> temp;
        public List<_MostDetails> _GetRecentTweets;
        public List<_MostDetails> _GetMostRetweetedTweets;
        public List<_MostDetails> _GetMostFavouriteTweets;

        public KeywordTweetsDetails(string keyword)
            {
            temp = GetTweets.GetTweetsForKeyowrds(keyword);

            _GetRecentTweets = new List<_MostDetails>();
            _GetMostRetweetedTweets = new List<_MostDetails>();
            _GetMostFavouriteTweets = new List<_MostDetails>();

            GetRecentTweets();
            GetMostRetweetedTweets();
            GetMostFavouriteTweets();
            }

        public void GetRecentTweets()
            {
            var tempVar = temp.Take(100).ToList();
            foreach (var item in tempVar)
                {
                _GetRecentTweets.Add(new _MostDetails
                    {
                    Favourite = item.FavoriteCount,
                    Retweet = item.RetweetCount,
                    TweetDate = item.CreatedAt,
                    TweetText = item.Text,
                    TwitteHandle = item.CreatedBy.ScreenName,
                    UserName = item.CreatedBy.Name,
                    ProfileIcon = item.CreatedBy.ProfileImageUrl,
                    url = item.Url
                    });
                }
            }

        public void GetMostRetweetedTweets()
            {
            if (temp != null)
                {
                var tempVar = temp.Where(x => x.RetweetCount > 0).OrderByDescending(x => x.RetweetCount).Take(100).ToList();

                foreach (var item in tempVar)
                    {
                    _GetMostRetweetedTweets.Add(new _MostDetails
                        {
                        Favourite = item.FavoriteCount,
                        Retweet = item.RetweetCount,
                        TweetDate = item.CreatedAt,
                        TweetText = item.Text,
                        TwitteHandle = item.CreatedBy.ScreenName,
                        UserName = item.CreatedBy.Name,
                        ProfileIcon = item.CreatedBy.ProfileImageUrl,
                        url = item.Url
                        });
                    }
                }
            }

        public void GetMostFavouriteTweets()
            {
            if (temp != null)
                {
                var tempVar = temp.Where(x => x.FavoriteCount > 0).OrderByDescending(x => x.FavoriteCount).Take(100).ToList();

                foreach (var item in tempVar)
                    {
                    _GetMostFavouriteTweets.Add(new _MostDetails
                        {
                        Favourite = item.FavoriteCount,
                        Retweet = item.RetweetCount,
                        TweetDate = item.CreatedAt,
                        TweetText = item.Text,
                        TwitteHandle = item.CreatedBy.ScreenName,
                        UserName = item.CreatedBy.Name,
                        ProfileIcon = item.CreatedBy.ProfileImageUrl,
                        url = item.Url
                        });
                    }
                }
            }
        }
    }