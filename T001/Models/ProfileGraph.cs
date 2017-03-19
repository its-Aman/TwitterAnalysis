using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tweetinvi.Models;

namespace T001.Models
    {
    public class ProfileGraph
        {
        IEnumerable<ITweet> temp;

        public GraphData _LineGraph_NoOfTweetsByDate;
        public GraphData _BarGraph_NoOfTweetsByDayOfWeek;
        public GraphData _BarGraph_NoOfTweetsByHour;

        public ProfileGraph(IUser user, DateTime fromDate, DateTime toDate)
            {
            temp = GetTweets.GetTweetsForKeyowrds(user, true)
                            .Where(x => toDate.Date >= x.CreatedAt.Date && x.CreatedAt.Date >= fromDate.Date);

            _LineGraph_NoOfTweetsByDate = new GraphData();
            _BarGraph_NoOfTweetsByDayOfWeek = new GraphData();
            _BarGraph_NoOfTweetsByHour = new GraphData();

            LineGraph_NoOfTweetsByDate(fromDate, toDate);
            _LineGraph_NoOfTweetsByDate.Run();

            BarGraph_NoOfTweetsByDayOfWeek();
            BarGraph_NoOfTweetsByHour();
            }

        public void LineGraph_NoOfTweetsByDate(DateTime fromDate, DateTime toDate)
            {
            bool IsPresent = false;
            _LineGraph_NoOfTweetsByDate.LabelText = "Tweets By Date";
            var flag = temp.OrderByDescending(x => x.CreatedAt.Date)
                           .GroupBy(x => x.CreatedAt.Date.ToString("dd. MMM-yy"));

            while (toDate.Date != fromDate.Date)
                {
                foreach (var item in flag)
                    {
                    IsPresent = false;
                    if (item.First().CreatedAt.Date == toDate.Date)
                        {
                        IsPresent = true;
                        _LineGraph_NoOfTweetsByDate.Label.Add(item.Key);
                        _LineGraph_NoOfTweetsByDate.Data.Add(item.Count());
                        break;
                        }
                    }
                if (!IsPresent)
                    {
                    _LineGraph_NoOfTweetsByDate.Label.Add(toDate.Date.ToString("dd. MMM-yy"));
                    _LineGraph_NoOfTweetsByDate.Data.Add(0);
                    }
                toDate = toDate.AddDays(-1);
                }
            }

        public void BarGraph_NoOfTweetsByDayOfWeek()
            {
            //TODO: amnac-please fix it
            }

        public void BarGraph_NoOfTweetsByHour()
            {
            //TODO: amnac-please fix it
            }
        }
    }