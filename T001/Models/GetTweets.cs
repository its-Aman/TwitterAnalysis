using System;
using System.Collections.Generic;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace T001.Models
    {
    public class GetTweets
        {
        private static IEnumerable<ITweet> _GetTweetsForKeyowrds_K;
        private static IEnumerable<ITweet> _GetTweetsForKeyowrds_U;
        private static IEnumerable<ITweet> _GetTweetsForKeyowrds_U_RTS;
        private static IEnumerable<ITweet> _GetTweetsForConnection;
        public static IEnumerable<ITweet> GetTweetsForKeyowrds(string keyword)
            {
            if (_GetTweetsForKeyowrds_K == null)
                {
                _GetTweetsForKeyowrds_K = Search.SearchTweets(new SearchTweetsParameters(keyword)
                    {
                    MaximumNumberOfResults = 500,
                    TweetSearchType = TweetSearchType.All,
                    Since = DateTime.Today,
                    SearchType = SearchResultType.Recent
                    });
                }
            return _GetTweetsForKeyowrds_K;
            }

        public static IEnumerable<ITweet> GetTweetsForKeyowrds(IUser user, bool includeRTS = false)
            {
            if (!includeRTS && _GetTweetsForKeyowrds_U == null)
                {
                _GetTweetsForKeyowrds_U = user.GetUserTimeline(new UserTimelineParameters
                    {
                    MaximumNumberOfTweetsToRetrieve = 250,
                    ExcludeReplies = false,
                    IncludeRTS = includeRTS,
                    IncludeContributorDetails = true,
                    IncludeEntities = true
                    });
                return _GetTweetsForKeyowrds_U;
                }
            else if (!includeRTS && _GetTweetsForKeyowrds_U != null)
                {
                return _GetTweetsForKeyowrds_U;
                }
            if (includeRTS && _GetTweetsForKeyowrds_U_RTS != null)
                {
                return _GetTweetsForKeyowrds_U_RTS;
                }
            else if (includeRTS)
                {
                _GetTweetsForKeyowrds_U_RTS = user.GetUserTimeline(new UserTimelineParameters
                    {
                    MaximumNumberOfTweetsToRetrieve = 250,
                    ExcludeReplies = false,
                    IncludeRTS = includeRTS,
                    IncludeContributorDetails = true,
                    IncludeEntities = true
                    });
                return _GetTweetsForKeyowrds_U_RTS;
                }
            else
                {
                return _GetTweetsForKeyowrds_U;
                }
            }

        public static IEnumerable<ITweet> GetTweetsForConnection(IUser user)
            {
            if (_GetTweetsForConnection == null)
                {
                _GetTweetsForConnection = Search.SearchTweets(new SearchTweetsParameters(user.ScreenName)
                    {
                    MaximumNumberOfResults = 250,
                    FilterTweetsNotContainingGeoInformation = false,
                    Filters = TweetSearchFilters.Replies
                    });
                }
            return _GetTweetsForConnection;
            }
        }
    }