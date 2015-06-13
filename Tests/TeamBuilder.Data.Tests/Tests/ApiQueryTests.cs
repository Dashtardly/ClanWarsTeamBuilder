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
    public class ApiQueryTests
    {

#region Data

        /// <summary>The assigned ID number for a registered WarGaming application.</summary>
        private const string APPLICATION_ID = "f539b9afae7d020157951f19cecf36ec";

        private const string CLAN_F3_ID = "1000007884";

        /// <summary>Sample API call response for information about a (specific) clan.</summary>
        private const string SAMPLE_CLAN_DATA = "WG_ClanInfo.json";

        /// <summary>Sample API call response for information about a (specific) clan.</summary>
        private const string SAMPLE_TANK_DATA = "WG_TankInfo.json";


        /// <summary>Path to where the data file can be found so it can be deployed into the
        ///          directory being used for the unit test run.</summary>
        /// <remarks>In VS2010 the DeploymentItem file path was either aboslute or relative but
        ///          in VS2013 it is now relative to the build output directory.</remarks>
        private const string SAMPLE_CLAN_FILE_PATH = "../../Data/" + SAMPLE_CLAN_DATA;

        /// <summary>Path to where the data file can be found so it can be deployed into the
        ///          directory being used for the unit test run.</summary>
        /// <remarks>In VS2010 the DeploymentItem file path was either aboslute or relative but
        ///          in VS2013 it is now relative to the build output directory.</remarks>
        private const string SAMPLE_TANK_FILE_PATH = "../../Data/" + SAMPLE_TANK_DATA;

#endregion Data

#region Test Methods

    #region GetClanInfo
        [TestMethod]
        [DeploymentItem( SAMPLE_CLAN_FILE_PATH )]
        public void GetClanInfo()
        {
            //Setup
            int expectedCount = 100;    //Determined by content in the sample file.

            string jsonData = TestHelpers.ReadFromFile( SAMPLE_CLAN_DATA );

            ApiWebClientFake wcFake = new ApiWebClientFake();
            wcFake.QueryResponse = jsonData;

            ApiQuery aq = new ApiQuery();
            aq.WebClient = wcFake;

            string clanData = aq.GetClanInfo( APPLICATION_ID, CLAN_F3_ID );

            ClanInfo ci = new ClanInfo();

            //Test
            bool result = ci.Load( clanData );

            //Validate
            if( null != ci.TrappedError )
            {
                Console.WriteLine( ci.TrappedError.ToString() );
            }
            Assert.IsTrue( result, "Failed to load the test data file content." );
            int actualCount = ci.Members.Count;
            Assert.IsTrue( ( expectedCount == actualCount ), string.Format( "Count mismatch. Expected {0} but have {1} members.", expectedCount, actualCount ) );
        }
    #endregion GetClanInfo

    #region GetClanInfoLive
        [TestMethod]
        public void GetClanInfoLive()
        {
            //Setup
            ApiQuery aq = new ApiQuery();
            aq.WebClient = new ApiWebClient();

            string clanData = aq.GetClanInfo( APPLICATION_ID, CLAN_F3_ID );
            TestHelpers.WriteToFile( clanData );

            ClanInfo ci = new ClanInfo();

            //Test
            bool result = ci.Load( clanData );

            //Validate
            if( null != ci.TrappedError )
            {
                Console.WriteLine( ci.TrappedError.ToString() );
            }
            Assert.IsTrue( result, "Failed to load the returned query data content." );
            int actualCount = ci.Members.Count;
            Console.WriteLine( "Query returned member count: " + actualCount );
            foreach( ClanMemberInfo cmi in ci.Members )
            {
                Console.WriteLine( "{0} is {1} who is a {2}", cmi.AccountID, cmi.Name, cmi.Role );
                Assert.IsNull( cmi.Tanks, "Tanks property has been set without a query/load." );
            }     
        }
    #endregion GetClanInfoLive

    #region GetTanksInfo
        [TestMethod]
        [DeploymentItem( SAMPLE_TANK_FILE_PATH )]
        public void GetTanksInfo()
        {
            //Setup
            int expectedCount = 396;    //Determined by content in the sample file.

            string jsonData = TestHelpers.ReadFromFile( SAMPLE_TANK_DATA );

            ApiWebClientFake wcFake = new ApiWebClientFake();
            wcFake.QueryResponse = jsonData;

            ApiQuery aq = new ApiQuery();
            aq.WebClient = wcFake;

            string tankData = aq.GetTanksInfo( APPLICATION_ID );

            Tanks tanks = new Tanks();

            //Test
            bool result = tanks.Load( tankData );

            //Validate
            if( null != tanks.TrappedError )
            {
                Console.WriteLine( tanks.TrappedError.ToString() );
            }
            Assert.IsTrue( result, "Failed to load the test data file content." );
            int actualCount = tanks.AllTanks.Count;
            Assert.IsTrue( ( expectedCount == actualCount ), string.Format( "Count mismatch. Expected {0} but have {1} tanks.", expectedCount, actualCount ) );
        }
    #endregion GetTanksInfo

    #region GetTanksInfoLive
        [TestMethod]
        public void GetTanksInfoLive()
        {
            //Setup
            ApiQuery aq = new ApiQuery();
            aq.WebClient = new ApiWebClient();

            string tankData = aq.GetTanksInfo( APPLICATION_ID );

            Tanks tanks = new Tanks();

            //Test
            bool result = tanks.Load( tankData );

            //Validate
            if( null != tanks.TrappedError )
            {
                Console.WriteLine( tanks.TrappedError.ToString() );
            }
            Assert.IsTrue( result, "Failed to load the returned query data content." );
            int actualCount = tanks.AllTanks.Count;
            Console.WriteLine( "Query returned tanks count: " + actualCount );
            foreach( TankInfo ti in tanks.AllTanks )
            {
                Console.WriteLine( "{0} is \"{1}\" which is a {2} Tier {3} {4}", ti.TankID, ti.Name, ti.Nation, ti.Tier, ti.TankType );
                Assert.IsNull( ti.Performance, "Performance property has been set without a query/load." );
            }
        }
    #endregion GetTanksInfoLive

#endregion Test Methods

    }
}
