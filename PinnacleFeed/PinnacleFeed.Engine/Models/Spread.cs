using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinnacleFeed.Engine.Models
{
    public class Spread
    {
        public long SportId { get; set; }

        public long LeagueId { get; set; }

        public long EventId { get; set; }

        public decimal HomeSpread { get; set; }

        public decimal AwaySpread { get; set; }

        public decimal HomePrice { get; set; }

        public decimal AwayPrice { get; set; }

        public bool IsAlt {get; set;}
    }
}
