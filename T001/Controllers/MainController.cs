using System;
using System.Web.Mvc;
using T001.Model_View;
using T001.Models;
using Tweetinvi.Models;

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
            ViewBag.ProfileGraph  = new ProfileGraph(currentUser, DateTime.Today.AddDays(-14), DateTime.Today);
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
        }
    }