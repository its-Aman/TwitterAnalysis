using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tweetinvi.Logic;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace T001.Models
    {
    public class GetUserMostHashtags
        {
        public List<_hashtagDetails> hashtagDetails;

        public GetUserMostHashtags(IUser user)
            {
            hashtagDetails = new List<_hashtagDetails>();
            }

        public GetUserMostHashtags(IUser user, DateTime fromDate, DateTime toDate) : this(user)
            {
            GetHashtag(user, fromDate, toDate);
            }

        public void GetHashtag(IUser user, DateTime FromDate, DateTime ToDate)
            {
            List<string> hashtags = new List<string>();

            var temp = user.GetUserTimeline(new UserTimelineParameters
                {
                MaximumNumberOfTweetsToRetrieve = 150,
                ExcludeReplies = false,
                IncludeEntities = true
                });

            if (temp != null)
                {
                temp.Where(x => x.Hashtags.Count > 0 && ToDate.Date >= x.CreatedAt.Date && x.CreatedAt.Date >= FromDate.Date)
                    .ToList()
                    .ForEach(x => x.Hashtags.ForEach(xs => hashtags.Add(xs.Text)));
                }

            hashtags.GroupBy(x => x)
                .OrderByDescending(x => x.Count())
                .ToList()
                .ForEach(item => hashtagDetails.Add(new _hashtagDetails
                    { hashtag = item.Key, hashtagsCount = item.Count() }
                ));

            }
        }
    }