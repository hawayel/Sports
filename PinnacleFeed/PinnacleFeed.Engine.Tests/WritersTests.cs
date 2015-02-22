using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinnacleFeed.Engine.Readers;
using PinnacleFeed.Engine.Database;
using System.Transactions;

namespace PinnacleFeed.Engine.Tests
{
    /// <summary>
    /// Summary description for WritersTests
    /// </summary>
    [TestClass]
    public class WritersTests
    {
        private readonly SportsReader _sportsReader = new SportsReader();

        private readonly LeaguesReader _leaguesReader = new LeaguesReader();

        private readonly EventsReader _eventsReader = new EventsReader();

        private readonly EventWriter _eventWriter = new EventWriter();

        private TransactionScope _transactionScope = null;

        public WritersTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void MyTestInitialize() 
        {
            _transactionScope = new TransactionScope();
        }
        
        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup() 
        {
            if (null != _transactionScope)
            {
                _transactionScope.Complete();
                _transactionScope.Dispose();
            }
        }
        //
        #endregion

        [TestMethod]
        public void CanWriteFeed()
        {
            var sport   = _sportsReader.Read().First(s => s.Name.Equals("soccer", StringComparison.OrdinalIgnoreCase));
            var league  = _leaguesReader.Read(sport.Id).First(l => l.HasContent);

            var events = _eventsReader.Read(sport.Id, league.Id);

            _eventWriter.Write(events);

        }
    }
}
