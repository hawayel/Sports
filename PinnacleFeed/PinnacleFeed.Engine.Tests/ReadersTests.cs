using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinnacleFeed.Engine.Readers;

namespace PinnacleFeed.Engine.Tests
{
    [TestClass]
    public class ReadersTests
    {
        private readonly SportsReader _sportsReader = new SportsReader();

        private readonly LeaguesReader _leaguesReader = new LeaguesReader();

        private readonly EventsReader _eventsReader = new EventsReader();

        [TestMethod]
        public void CanReadSports()
        {
            var sports = _sportsReader.Read();

            Assert.IsTrue(sports.Length > 0);
        }

        [TestMethod]
        public void CanReadLeagues()
        {
            var sports  = _sportsReader.Read();
            var leagues = _leaguesReader.Read(sports[0].Id);

            Assert.IsTrue(leagues.Length > 0);
        }

        [TestMethod]
        public void CanReadEvents()
        {
            var sport   = _sportsReader.Read().First(s => s.Name.Equals("soccer", StringComparison.OrdinalIgnoreCase));            
            var league  = _leaguesReader.Read(sport.Id).First(l => l.HasContent);

            var events = _eventsReader.Read(sport.Id, league.Id);

            Assert.IsTrue(events.Length > 0);
        }


    }
}
