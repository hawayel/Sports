using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PinnacleFeed.Engine.Models
{
    public class Event 
    {
        public long Id { get; set; }

        public long SportId { get; set; }

        public long LeagueId { get; set; }

        public DateTime StartTime { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public Spread[] Spreads { get; set; }

        public Total[] Totals { get; set; }
    }
}
