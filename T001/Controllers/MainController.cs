using System;
using System.Linq;
using System.Web.Mvc;
using T001.Model_View;
using T001.Models;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace T001.Controllers
    {
    public class MainController : Controller
        {
        IUser currentUser;

        public MainController()
            {
            currentUser = Tweetinvi.User.GetUserFromScreenName(MyUser.User.ScreenName);
            }
        // GET: Main
        public new ActionResult Profile()
            {
            ViewBag.Graph = new Graph(currentUser, DateTime.Today.AddDays(-14), DateTime.Today);
            ViewBag.ViewProfile = new View_Profile(currentUser);
            ViewBag.User = MyUser.User;
            return View(ViewBag);
            }

        public ActionResult Connections()
            {
            return View(new View_Connections(currentUser));
            }

        public ActionResult Keyword(string keyword = "Love")
            {
            return View(new View_Keyword(currentUser, keyword));
            }

        public ActionResult Sentiment()
            {
            return null;
            }
        public ActionResult HashtagsAnalysis()
            {
            var t = Trends.GetTrendsAt(23424848);
            return View(t.Trends);
            }
        public PartialViewResult TrendResult(string trend)
            {
            var tweets = Search.SearchTweets(new SearchTweetsParameters(trend)
                {
                SearchType = SearchResultType.Mixed,
                MaximumNumberOfResults=10
                }).ToList();
            return PartialView("TrendPartialView", tweets);
            }
        }
    }