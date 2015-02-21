using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinnacleFeed.Engine.Models
{
    public class Total
    {
        public long SportId { get; set; }

        public long LeagueId { get; set; }

        public long EventId { get; set; }

        public decimal Points { get; set; }

        public decimal OverPrice { get; set; }

        public decimal UnderPrice { get; set; }

        public bool IsAlt { get; set; }
    }
}
