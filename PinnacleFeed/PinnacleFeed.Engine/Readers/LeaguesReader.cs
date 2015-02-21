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
    public class LeaguesReader
    {
        private readonly string FeedUrl = @"https://api.pinnaclesports.com/v1/leagues?sportid={0}";

        public League[] Read(long sportId)
        {
            var wc = new WebClient();

            wc.Headers[HttpRequestHeader.Authorization] = BasicAuth.HeaderValue;
            string feed = Encoding.UTF8.GetString(wc.DownloadData(string.Format(FeedUrl, sportId)));

            var xml = XElement.Parse(feed);

            var leagues = xml.Element("leagues")
                .Elements("league")
                .Select(s => new League() {
                    Id = Convert.ToInt32(s.Attribute("id").Value),
                    SportId = sportId,
                    Name = s.Value,
                    HasContent = (1 == Convert.ToInt32(s.Attribute("feedContents").Value))
                });

            return leagues.ToArray();
        }

    }
}
