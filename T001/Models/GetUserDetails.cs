using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Tweetinvi;
using Tweetinvi.Logic;
using Tweetinvi.Models;
using Tweetinvi.Models.Entities;
using Tweetinvi.Parameters;

namespace T001.Models
    {
    public class GetUserDetails
        {
        private string keyword;
        private string partialUrl = "https://twitter.com/";
        private IEnumerable<ITweet> temp;

        //profile
        public List<_UserDetails> _GetUserMostRetweet;
        public List<_UserDetails> _GetUserMostRepliesTo;
        public List<_UserDetails> _GetUserMostMention;

        //connections
        public List<_UserDetails> _MostFollowedUsersThatMentionedYou;
        public List<_UserDetails> _UsersWhoMentionedYouTheMost;

        public GetUserDetails(IUser user)
            {
            _GetUserMostRetweet = new List<_UserDetails>();
            _GetUserMostRepliesTo = new List<_UserDetails>();
            _GetUserMostMention = new List<_UserDetails>();

            _MostFollowedUsersThatMentionedYou = new List<_UserDetails>();
            _UsersWhoMentionedYouTheMost = new List<_UserDetails>();
            }

        public GetUserDetails(IUser user, DateTime fromDate, DateTime toDate) : this(user)
            {
            temp = GetTweets.GetTweetsForKeyowrds(user);

            GetUserMostRetweet(user, fromDate, toDate);
            GetUserMostRepliesTo(user, fromDate, toDate);
            GetUserMostMention(user, fromDate, toDate);

            temp = GetTweets.GetTweetsForConnection(user);

            UsersWhoMentionedYouTheMost(user);
            MostFollowedUsersThatMentionedYou(user);
            }


        //For Profile
        #region Profile

        public void GetUserMostRetweet(IUser user, DateTime FromDate, DateTime ToDate)
            {
            List<IUser> mostRT = new List<IUser>();
            var temp = Search.SearchTweets(new SearchTweetsParameters(user.ScreenName)
                {
                TweetSearchType = TweetSearchType.RetweetsOnly,
                Since = FromDate.Date,
                Until = ToDate.Date
                }).ToList();

            if (temp != null)
                {
                temp.ForEach(x => mostRT.Add(x.RetweetedTweet.CreatedBy));
                mostRT.GroupBy(x => x).OrderByDescending(x => x.Count()).ToList().ForEach(item =>
                {
                   _GetUserMostRetweet.Add(new _UserDetails
                        {
                        Follower = item.Key.FollowersCount,
                        Following = item.Key.FriendsCount,
                        Retweet = item.Count(),
                        tweet = item.Key.StatusesCount,
                        TwitteHandle = item.Key.ScreenName,
                        UserName = item.Key.Name,
                        ProfileIcon = item.Key.ProfileImageUrl,
                        url = partialUrl + item.Key.ScreenName
                        });
                });
                }
            }

        public void GetUserMostRepliesTo(IUser user, DateTime FromDate, DateTime ToDate)
            {
            List<ITweet> filteredTweets = new List<ITweet>();
            List<_UserDetails> details = new List<_UserDetails>();

            if (temp != null)
                {
                foreach (var item in temp)
                    {
                    if ((item.InReplyToScreenName != null) &&
                        (ToDate.Date >= item.CreatedAt.Date) && (item.CreatedAt.Date >= FromDate.Date))
                        {
                        filteredTweets.Add(item);
                        }
                    }

                var groupedTweets = filteredTweets.GroupBy(x => x.InReplyToScreenName);

                foreach (var item in groupedTweets)
                    {
                    var replyToUser = Tweetinvi.User.GetUserFromScreenName(item.Key);

                    if (replyToUser != null)
                        {
                        details.Add(new _UserDetails
                            {
                            Follower = replyToUser.FollowersCount,
                            Following = replyToUser.FriendsCount,
                            Retweet = item.Count(),
                            tweet = replyToUser.StatusesCount,
                            TwitteHandle = replyToUser.ScreenName,
                            UserName = replyToUser.Name,
                            ProfileIcon = replyToUser.ProfileImageUrl,
                            url = partialUrl + replyToUser.ScreenName,
                            Replies=item.Count()
                            });
                        }
                    }
                _GetUserMostRepliesTo = details.OrderByDescending(x => x.Replies).ToList();
                }
            }

        public void GetUserMostMention(IUser user, DateTime FromDate, DateTime ToDate)
            {
            List<IUserMentionEntity> mentionUsers = new List<IUserMentionEntity>();
            List<_UserDetails> details = new List<_UserDetails>();

            if (temp != null)
                {
                foreach (var item in temp)
                    {
                    if ((item.Entities.UserMentions.Count > 0) &&
                        (ToDate.Date >= item.CreatedAt.Date) && (item.CreatedAt.Date >= FromDate.Date))
                        {
                        item.Entities.UserMentions.ForEach(x => mentionUsers.Add(x));
                        }
                    }

                var grouping = mentionUsers.GroupBy(x => x.ScreenName);
                foreach (var item in grouping)
                    {
                    var filteredMentionUser = Tweetinvi.User.GetUserFromScreenName(item.Key);

                    if (filteredMentionUser != null)
                        {
                        details.Add(new _UserDetails
                            {
                            ProfileIcon = filteredMentionUser.ProfileImageUrl,
                            UserName = filteredMentionUser.Name,
                            TwitteHandle = filteredMentionUser.ScreenName,
                            Mention = item.Count(),
                            Follower = filteredMentionUser.FollowersCount,
                            Following = filteredMentionUser.FriendsCount,
                            tweet = filteredMentionUser.StatusesCount,
                            url = partialUrl + filteredMentionUser.ScreenName
                            });
                        }
                    }
                _GetUserMostMention = details.OrderByDescending(x => x.Mention).ToList();
                }
            }

        #endregion

        //For Connection
        #region Connection
        public void MostFollowedUsersThatMentionedYou(IUser user)
            {
            _MostFollowedUsersThatMentionedYou = _UsersWhoMentionedYouTheMost.OrderByDescending(x => x.Follower).ToList();
            }

        public void UsersWhoMentionedYouTheMost(IUser user)
            {
            List<IUser> tempVar = new List<IUser>();

            temp.ToList().Where(x => x.CreatedBy.ScreenName != user.ScreenName).ToList()
               .ForEach(x => tempVar.Add(x.CreatedBy));

            tempVar.GroupBy(x => x.ScreenName)
                   .OrderByDescending(x => x.Count())
                   .ToList()
                   .ForEach(item =>
                   {
                       var foo = item.First();
                       _UsersWhoMentionedYouTheMost.Add(new _UserDetails
                           {
                           Mention = item.Count(),
                           Follower = foo.FollowersCount,
                           Following = foo.FriendsCount,
                           ProfileIcon = foo.ProfileImageUrl,
                           UserName = foo.Name,
                           TwitteHandle = foo.ScreenName,
                           tweet = foo.StatusesCount,
                           url = foo.Url
                           });
                   });
            }
        #endregion

        }
    }