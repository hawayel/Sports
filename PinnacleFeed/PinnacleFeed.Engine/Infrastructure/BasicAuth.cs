using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace PinnacleFeed.Engine.Infrastructure
{
    public class BasicAuth
    {
        private static Lazy<string> LazyHeaderValue = new Lazy<string>(GetBasicAuthCreds);

        private static string GetBasicAuthCreds()
        {
            var userFolder  = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var devFolder   = Path.Combine(userFolder, "Dev");
            var configFile  = Path.Combine(devFolder, "feed.config");

            return string.Format("Basic {0}", Convert.ToBase64String(Encoding.ASCII.GetBytes(File.ReadAllText(configFile))));
        }

        public static string HeaderValue
        {
            get 
            {
                return LazyHeaderValue.Value;
            }
        }
    }
}
