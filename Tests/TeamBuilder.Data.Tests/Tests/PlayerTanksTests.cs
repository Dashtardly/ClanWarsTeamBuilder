#region Copyright (C) 2015  M.T. Lansdaal and License
/*
    CWTeamBuilder - Help Select Game Players for Clan Wars Team Battle Games in World of Tanks (WoT).
    Copyright (C) 2015  M.T. Lansdaal
 
    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
#endregion Copyright (C) 2015  M.T. Lansdaal and License

#region Change History

// DATE------  CHANGED BY---  CHANGEID#  DESCRIPTION---------------------------
// 2015-05-17  M. Lansdaal    n/a        Initial version.
// 2015-06-13  M. Lansdaal    n/a        PlayerInfo deprecated. Replaced by PerformanceInfo
//

#endregion

#region Using Directives

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.IO;    //for File, Path

using WoT.Contributed.TeamBuilder.Data;

#endregion


namespace TeamBuilder.Data.Tests
{
    [TestClass]
    public class PlayerTanksTests
    {

#region Data

        /// <summary>Sample API call response for information about the tank (vehicles) in the game.</summary>
        private const string SAMPLE_TANKS_DATA = "WG_TankInfo.json";

        /// <summary>Path to where the data file can be found so it can be deployed into the
        ///          directory being used for the unit test run.</summary>
        /// <remarks>In VS2010 the DeploymentItem file path was either aboslute or relative but
        ///          in VS2013 it is now relative to the build output directory.</remarks>
        private const string SAMPLE_TANKS_FILE_PATH = "../../Data/" + SAMPLE_TANKS_DATA;




        /// <summary>Sample API call response for performance information about a player.</summary>
        private const string SAMPLE_PLAYER_PERF_DATA = "WG_PlayerPerformance.json";

        /// <summary>Sample API call response for information about the tanks a player owns.</summary>
        private const string SAMPLE_PLAYER_TANK_DATA = "WG_PlayerTanks.json";


        /// <summary>Player account ID that is in the sample data.</summary>
        private const int SAMPLE_PLAYER_ID = 1000029478;

        /// <summary>Path to where the data file can be found so it can be deployed into the
        ///          directory being used for the unit test run.</summary>
        /// <remarks>In VS2010 the DeploymentItem file path was either aboslute or relative but
        ///          in VS2013 it is now relative to the build output directory.</remarks>
        private const string SAMPLE_DATA_PERF_FILE_PATH = "../../Data/" + SAMPLE_PLAYER_PERF_DATA;

        /// <summary>Path to where the data file can be found so it can be deployed into the
        ///          directory being used for the unit test run.</summary>
        /// <remarks>In VS2010 the DeploymentItem file path was either aboslute or relative but
        ///          in VS2013 it is now relative to the build output directory.</remarks>
        private const string SAMPLE_DATA_TANK_FILE_PATH = "../../Data/" + SAMPLE_PLAYER_TANK_DATA;


#endregion Data

#region Test Methods

    #region SampleDataFilesPresent
        [TestMethod]
        [DeploymentItem( SAMPLE_TANKS_FILE_PATH )]
        [DeploymentItem( SAMPLE_DATA_PERF_FILE_PATH )]
        [DeploymentItem( SAMPLE_DATA_TANK_FILE_PATH )]
        public void SampleDataFilesPresent()
        {
            //Setup
            string cwd = System.Environment.CurrentDirectory;
            string tanksFilePath = Path.Combine( cwd, SAMPLE_TANKS_DATA );
            string perfFilePath  = Path.Combine( cwd, SAMPLE_PLAYER_PERF_DATA );
            string tankFilePath  = Path.Combine( cwd, SAMPLE_PLAYER_TANK_DATA );

            //Test
            bool tanksExists = File.Exists( tanksFilePath );
            bool perfExists = File.Exists( perfFilePath );
            bool tankExists = File.Exists( tankFilePath );

            //Validate
            Assert.IsTrue( tanksExists, "Setup error. Check that file deployment is setup." );
            Assert.IsTrue( perfExists, "Setup error. Check that file deployment is setup." );
            Assert.IsTrue( tankExists, "Setup error. Check that file deployment is setup." );
        }
    #endregion SampleDataFilesPresent

    #region LoadTanksOwned
        [TestMethod]
        [DeploymentItem( SAMPLE_TANKS_FILE_PATH )]
        [DeploymentItem( SAMPLE_DATA_TANK_FILE_PATH )]
        public void LoadTanksOwned()
        {
            //Setup
            int expectedCount = 133;

            //  Get the reference information about all the tanks in the game.
            string tanksData = TestHelpers.ReadFromFile( SAMPLE_TANKS_DATA );
            Tanks tanks = new Tanks();
            bool cwDataLoaded = tanks.Load( tanksData );
            Assert.IsTrue( cwDataLoaded, "Setup error. Failed to load reference TANKS data." );

            //  Create and intialize an instance for a specific player
            PlayerTanks pt = new PlayerTanks();
            pt.AccountID = SAMPLE_PLAYER_ID;

            //      Get the list of tanks that they own.
            string tankData = TestHelpers.ReadFromFile( SAMPLE_PLAYER_TANK_DATA );
            
            //Test
            bool result = pt.LoadTanksOwned( tankData );

            //Validate
            if( null != pt.TrappedError )
            {
                Console.WriteLine( pt.TrappedError.ToString() );
            }
            Assert.IsTrue( result, "Failed to load owned TANKS data." );
            int actualCount = pt.TanksOwned.Count;
            Console.WriteLine( "Player {0} owns {1} tanks.", SAMPLE_PLAYER_ID, actualCount );
            Assert.IsTrue( ( expectedCount == actualCount ), string.Format( "Count mismatch. Expected {0} but have {1} tanks.", expectedCount, actualCount ) );
        }
    #endregion LoadTanksOwned

    #region LoadClanWarsTanks
        [TestMethod]
        [DeploymentItem( SAMPLE_TANKS_FILE_PATH )]
        [DeploymentItem( SAMPLE_DATA_TANK_FILE_PATH )]
        public void LoadClanWarsTanks()
        {
            //Setup
            int expectedCount = 15;    //Determined by content in the sample file.

            //  Get the reference information about all the tanks in the game.
            string tanksData = TestHelpers.ReadFromFile( SAMPLE_TANKS_DATA );
            Tanks tanks = new Tanks();
            bool cwDataLoaded = tanks.Load( tanksData );
            Assert.IsTrue( cwDataLoaded, "Setup error. Failed to load reference TANKS data." );

            //  Create and intialize an instance for a specific player
            PlayerTanks pt = new PlayerTanks();
            pt.AccountID = SAMPLE_PLAYER_ID;

            //      Get the list of tanks that they own.
            string tankData = TestHelpers.ReadFromFile( SAMPLE_PLAYER_TANK_DATA );
            bool toLoaded = pt.LoadTanksOwned( tankData );
            Assert.IsTrue( toLoaded, "Setup error. Failed to load owned TANKS data." );

            //Test
            bool cwLoaded = pt.LoadClanWarsTanks( tanks );

            //Validate
            if( null != pt.TrappedError )
            {
                Console.WriteLine( pt.TrappedError.ToString() );
            }
            Assert.IsTrue( cwLoaded, "Failed to load up CW TANKS." );
            int actualCount = pt.ClanWarsTanks.Count;
            Console.WriteLine( "Have {0} CW Tanks.", actualCount );
            Assert.IsTrue( ( expectedCount == actualCount ), string.Format( "Count mismatch. Expected {0} but have {1} tanks.", expectedCount, actualCount ) );
        }
    #endregion LoadClanWarsTanks

    #region LoadTankPerformance
        [TestMethod]
        [DeploymentItem( SAMPLE_TANKS_FILE_PATH )]
        [DeploymentItem( SAMPLE_DATA_PERF_FILE_PATH )]
        [DeploymentItem( SAMPLE_DATA_TANK_FILE_PATH )]
        public void LoadsJsonDataPerf()
        {
            //Setup
            int expectedCount = 15;    //Determined by content in the sample file.

            //  Get the reference information about all the tanks in the game.
            string tanksData = TestHelpers.ReadFromFile( SAMPLE_TANKS_DATA );
            Tanks tanks = new Tanks();
            bool cwDataLoaded = tanks.Load( tanksData );
            Assert.IsTrue( cwDataLoaded, "Setup error. Failed to load reference TANKS data." );

            //  Create and intialize an instance for a specific player
            PlayerTanks pt = new PlayerTanks();
            pt.AccountID = SAMPLE_PLAYER_ID;

            //      Get the list of tanks that they own.
            string tankData = TestHelpers.ReadFromFile( SAMPLE_PLAYER_TANK_DATA );
            bool toLoaded = pt.LoadTanksOwned( tankData );
            Assert.IsTrue( toLoaded, "Setup error. Failed to load owned TANKS data." );

            //      Filter it down to the list of CW usable tanks
            bool cwLoaded = pt.LoadClanWarsTanks( tanks );
            Assert.IsTrue( cwLoaded, "Setup error. Failed to load up CW TANKS." );

            //      Get their performance information.
            string perfData = TestHelpers.ReadFromFile( SAMPLE_PLAYER_PERF_DATA );

            //Test
            bool result = pt.LoadTankPerformance( perfData );

            //Validate
            if( null != pt.TrappedError )
            {
                Console.WriteLine( pt.TrappedError.ToString() );
            }
            Assert.IsTrue( result, "Failed to load up performance data on tanks." );

            //TODO: Figure out a verification on what was loaded.

            //int actualCount = pt.ClanWarsTanks.Count;
            //Console.WriteLine( "Have {0} CW Tanks.", actualCount );
            //Assert.IsTrue( ( expectedCount == actualCount ), string.Format( "Count mismatch. Expected {0} but have {1} tanks.", expectedCount, actualCount ) );
        }
    #endregion LoadTankPerformance

#endregion Test Methods

    }
}
