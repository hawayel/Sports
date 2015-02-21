using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinnacleFeed.Engine.Models
{
    public class League
    {
        public long Id { get; set; }

        public long SportId { get; set; }

        public string Name { get; set; }

        public bool HasContent { get; set; }
    }
}
