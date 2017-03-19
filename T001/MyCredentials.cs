using Tweetinvi.Models;

namespace T001
    {
    public static class MyCredentials
        {
        public static string CONSUMER_KEY = "g4LL3rCXIYpnkCIdpkPqeO2Qi";
        public static string CONSUMER_SECRET = "5RW0SNhb63ipE2UCJDSx6dn1crkOtXSMN47ajS0mvPt3K4uCHZ";
        public static string ACCESS_TOKEN = "3104816430-axdqPGWKYAbNmevERwNMGMan3Kyek67iI7Hgj4o";
        public static string ACCESS_TOKEN_SECRET = "hqjlgqabvTfGycFWFZ5sDaq3Eo1X5D2f54YhjPx2o8cmF";


        public static ITwitterCredentials GenerateCredentials()
            {
            return new TwitterCredentials(CONSUMER_KEY, CONSUMER_SECRET, ACCESS_TOKEN, ACCESS_TOKEN_SECRET);
            }
        }
    }