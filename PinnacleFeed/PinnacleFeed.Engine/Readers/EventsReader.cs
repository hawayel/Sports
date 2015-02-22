using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PinnacleFeed.Engine.Models;
using System.Net;
using PinnacleFeed.Engine.Infrastructure;
using System.Xml.Linq;
using System.Globalization;

namespace PinnacleFeed.Engine.Readers
{
    public class EventsReader
    {   
        private readonly string FeedUrl = @"https://api.pinnaclesports.com/v1/feed?sportid={0}&leagueid={1}&oddsFormat=1&last={2}";

        private string Last = "0";

        public Event[] Read(long sportId, long leagueId)
        {
            var wc = new WebClient();

            wc.Headers[HttpRequestHeader.Authorization] = BasicAuth.HeaderValue;
            string feed = Encoding.UTF8.GetString(wc.DownloadData(string.Format(FeedUrl, sportId, leagueId, Last)));

            var xml = XElement.Parse(feed);

            Last = xml.Element("fd").Element("fdTime").Value;

            var sport   = xml.Element("fd").Element("sports").Element("sport");
            var league  = sport.Element("leagues").Element("league");

            return league.Element("events").Elements("event").Select(e => ParseEvent(e, sportId, leagueId)).ToArray();
        }

        private Event ParseEvent(XElement xEvent, long sportId, long leagueId)
        {
            var xPeriod = xEvent.Element("periods").Elements("period").First(e => e.Element("description").Value.Equals("Match"))
                ?? xEvent.Element("periods").Elements("period").First();

            var match = new Event() { SportId = sportId, LeagueId = leagueId}; 
            
            match.Id = Convert.ToInt32(xEvent.Element("id").Value);
            match.StartTime = DateTime.Parse(xEvent.Element("startDateTime").Value, null, DateTimeStyles.RoundtripKind).AddMinutes(1);
            match.HomeTeam = xEvent.Element("homeTeam").Element("name").Value;
            match.AwayTeam = xEvent.Element("awayTeam").Element("name").Value;
            match.Spreads = GetSpreads(match, xPeriod);
            match.Totals = GetTotals(match, xPeriod);

            return match;
        }


        private static Spread[] GetSpreads(Event match, XElement xPeriod)
        {
            var spreads = new Spread[] { };

            if (null == xPeriod)
            {
                return spreads;
            }

            var xSpreads = xPeriod.Element("spreads");

            if(null == xSpreads || !xSpreads.HasElements)
            {
                return spreads;
            }

            spreads = xSpreads.Elements("spread")
                .Select(s => new Spread() {
                    SportId = match.SportId,
                    LeagueId = match.LeagueId,
                    EventId = match.Id,
                    HomeSpread = Convert.ToDecimal(s.Element("homeSpread").Value),
                    AwaySpread = Convert.ToDecimal(s.Element("awaySpread").Value),
                    HomePrice = Convert.ToDecimal(s.Element("homePrice").Value),
                    AwayPrice = Convert.ToDecimal(s.Element("awayPrice").Value),
                    IsAlt = (null != s.Attribute("altLineId")),
                }).ToArray();

            return spreads;
        }

        private static Total[] GetTotals(Event match, XElement xPeriod)
        {
            var totals = new Total[] { };

            if (null == xPeriod) 
            {
                return totals;
            }

            var xTotals = xPeriod.Element("totals");
            if(null == xTotals || !xTotals.HasElements)
            {
                return totals;
            }

            totals = xTotals.Elements("total")
                .Select(s => new Total() {
                    SportId = match.SportId,
                    LeagueId = match.LeagueId,
                    EventId = match.Id,
                    Points = Convert.ToDecimal(s.Element("points").Value),
                    OverPrice = Convert.ToDecimal(s.Element("overPrice").Value),
                    UnderPrice = Convert.ToDecimal(s.Element("underPrice").Value),
                    IsAlt = (null != s.Attribute("altLineId")),
                }).ToArray();

            return totals;
        }
    }
}
