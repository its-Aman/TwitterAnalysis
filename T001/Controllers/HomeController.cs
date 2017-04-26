using Tweetinvi;
using Tweetinvi.Models;
using System.Web.Mvc;
using System.Linq;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using T001.Models;
using T001.Logic;
using T001.Model_View;
using Tweetinvi.Logic;

namespace T001.Controllers
    {
    public class HomeController : Controller
        {
        public ActionResult Index()
            {
            return View();
            }

        public ActionResult About()
            {
            ViewBag.Message = "Analysis of Twitter Hash-tag, words for content analysis.";

            return View();
            }

        public ActionResult Contact()
            {
            ViewBag.Message = "We're Listening you!!!";

            return View();
            }

        // GET: TwitterAuth
        public ActionResult TwitterAuth()
            {
            var appCreds = new ConsumerCredentials(MyCredentials.CONSUMER_KEY, MyCredentials.CONSUMER_SECRET);
            var redirectURL = "http://" + Request.Url.Authority + "/Home/ValidateTwitterAuth";
            var authenticationContext = AuthFlow.InitAuthentication(appCreds, redirectURL);

            return new RedirectResult(authenticationContext.AuthorizationURL);
            }

        // GET: ValidateTwitterAuth
        public ActionResult ValidateTwitterAuth()
            {
            MyUser.VerifierCode = Request.Params.Get("oauth_verifier");
            MyUser.AuthorizationId = Request.Params.Get("authorization_id");

            if (MyUser.VerifierCode != null)
                {
                MyUser.UserCreds = AuthFlow.CreateCredentialsFromVerifierCode(MyUser.VerifierCode, MyUser.AuthorizationId) as TwitterCredentials;
                MyUser.User = Tweetinvi.User.GetAuthenticatedUser(MyUser.UserCreds) as AuthenticatedUser;

                MyUser.AuthCredentials = Auth.SetUserCredentials(MyCredentials.CONSUMER_KEY, MyCredentials.CONSUMER_SECRET, MyUser.User.Credentials.AccessToken, MyUser.User.Credentials.AccessTokenSecret) as TwitterCredentials;
                }

            Response.Redirect("~/Main/Profile");

            var _Iuser = Tweetinvi.User.GetUserFromScreenName("amit_1683");
            View_Profile vP = new View_Profile(_Iuser);
            View_Connections vC = new View_Connections(_Iuser);
            View_Keyword vK = new View_Keyword(_Iuser, "Love");
            ProfileGraph pg = new ProfileGraph(_Iuser, DateTime.Today.AddDays(-14), DateTime.Today);

            ViewBag.View_Profile = vP;
            ViewBag.View_Connections = vC;
            ViewBag.View_Keyword = vK;
            ViewBag.User = MyUser.User;
            ViewBag.ProfileGraph = pg;
            return View(ViewBag);
            }
        }
    }