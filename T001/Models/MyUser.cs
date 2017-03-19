using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tweetinvi.Logic;
using Tweetinvi.Models;

namespace T001.Models
    {
    public class MyUser
        {
        public static string VerifierCode { get; set; }
        public static string AuthorizationId { get; set; }
        public static TwitterCredentials UserCreds { get; internal set; }
        public static AuthenticatedUser User { get; internal set; }
        public static TwitterCredentials AuthCredentials { get; internal set; }
        }
    }