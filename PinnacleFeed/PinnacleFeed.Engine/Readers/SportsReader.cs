using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PinnacleFeed.Engine.Models;
using System.Net;
using PinnacleFeed.Engine.Infrastructure;
using System.Xml.Linq;

namespace PinnacleFeed.Engine.Readers
{
    public class SportsReader
    {
        private const string FeedUrl = @"https://api.pinnaclesports.com/v1/sports";

        public Sport[] Read()
        {
            var wc = new WebClient();

            wc.Headers[HttpRequestHeader.Authorization] = BasicAuth.HeaderValue;
            string feed = Encoding.UTF8.GetString(wc.DownloadData(FeedUrl));

            var xml = XElement.Parse(feed);

            var sports = xml.Element("sports")
                .Elements("sport")
                .Select(s => new Sport() 
                { 
                    Id = Convert.ToInt32(s.Attribute("id").Value),
                    Name = s.Value,
                    HasContent = (1 == Convert.ToInt32(s.Attribute("feedContents").Value))
                });

            return sports.ToArray();
        }
    }
}
