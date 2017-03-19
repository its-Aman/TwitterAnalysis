using System.Collections.Generic;
using System.Linq;
using Tweetinvi.Models;

namespace T001.Models
    {
    public class KeywordUserDetails
        {
        private IEnumerable<ITweet> temp;
        public int userCount;

        public List<_KeywordUserDetails> _GetMostFollowedUsers;

        public KeywordUserDetails(string keyword)
            {
            temp = GetTweets.GetTweetsForKeyowrds(keyword);

            userCount = temp.Count();
            _GetMostFollowedUsers = new List<_KeywordUserDetails>();

            GetMostFollowedUsers(keyword);

            }

        public void GetMostFollowedUsers(string keyword)
            {
            var tempVar = temp.ToList()
                .Where(x => x.CreatedBy.FollowersCount > 0 && !x.IsRetweet)
                .OrderByDescending(x => x.CreatedBy.FollowersCount).Take(100)
                .Select(x => new { user = x.CreatedBy });

            foreach (var item in tempVar)
                {
                _GetMostFollowedUsers.Add(new _KeywordUserDetails
                    {
                    Follower = item.user.FollowersCount,
                    Following = item.user.FriendsCount,
                    tweet = item.user.StatusesCount,
                    UserName = item.user.Name,
                    TwitteHandle = item.user.ScreenName,
                    use = 1,
                    ProfileIcon = item.user.ProfileImageUrl
                    });
                }
            }
        }

    public class _KeywordUserDetails : _UserDetails
        {
        public int use;
        }
    }