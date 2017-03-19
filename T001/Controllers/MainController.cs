using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace T001.Controllers
    {
    public class MainController : Controller
        {
        // GET: Main
        public new ActionResult Profile()
            {
            return View();
            }

        public ActionResult Connections()
            {
            return View();
            }

        public ActionResult Keyword()
            {
            return View();
            }
        }
    }