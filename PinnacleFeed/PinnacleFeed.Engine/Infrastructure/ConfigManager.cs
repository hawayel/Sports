using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinnacleFeed.Engine.Infrastructure
{
    class ConfigManager
    {
        private const string ConnectionString = @"Server=.\SQLExpress;Database=Pinnacle.Feed.Database;Trusted_Connection=Yes;";

        public static string GetConnectionString()
        {
            return ConnectionString;
        }
    }
}
