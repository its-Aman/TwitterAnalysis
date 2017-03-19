using System.Collections.Generic;
using System.Linq;
using Tweetinvi;
using Tweetinvi.Models;

namespace T001.Logic
    {
    public class GraphCalculation
        {

        public static IEnumerable<ITweet> tweets { get; set; }

        public static List<string> label = new List<string>();
        public static List<double> data = new List<double>();

        public GraphCalculation(string username)
            {
            GetTweets(username);
            }

        public static void GetTweets(string username)
            {
            tweets = User.GetUserFromScreenName(username).GetUserTimeline();
            }

        public void GraphDataForDay()
            {
            var temp = (
                        from filteredTweet
                        in tweets
                        group filteredTweet
                        by filteredTweet.CreatedAt.Date.ToString("dd. MMM-yy")
                        ).Take(10);

            foreach (var item in temp)
                {
                label.Add(item.Key.ToString());
                data.Add(item.Count());
                }
            }
        }
    }