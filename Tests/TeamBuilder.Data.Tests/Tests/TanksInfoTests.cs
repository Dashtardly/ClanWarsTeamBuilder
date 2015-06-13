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

using System.Collections.Generic;   //for List<T>
using System.IO;                    //for File, Path

using WoT.Contributed.TeamBuilder.Data;

#endregion


namespace TeamBuilder.Data.Tests
{
    [TestClass]
    public class TanksInfoTests
    {

#region Data

        /// <summary>Sample API call response for information about the tank (vehicles) in the game.</summary>
        private const string SAMPLE_TANK_DATA = "WG_TankInfo.json";

        /// <summary>Path to where the data file can be found so it can be deployed into the
        ///          directory being used for the unit test run.</summary>
        /// <remarks>In VS2010 the DeploymentItem file path was either aboslute or relative but
        ///          in VS2013 it is now relative to the build output directory.</remarks>
        private const string SAMPLE_DATA_FILE_PATH = "../../Data/" + SAMPLE_TANK_DATA;

#endregion Data

#region Test Methods

    #region SampleDataFilePresent
        [TestMethod]
        [DeploymentItem( SAMPLE_DATA_FILE_PATH )]
        public void SampleDataFilePresent()
        {
            //Setup
            string cwd = System.Environment.CurrentDirectory;
            string dataFilePath = Path.Combine( cwd, SAMPLE_TANK_DATA );

            //Test
            bool exists = File.Exists( dataFilePath );

            //Validate
            Assert.IsTrue( exists, "Setup error. Check that file deployment is setup." );
        }
    #endregion SampleDataFilePresent

    #region LoadsJsonData
        [TestMethod]
        [DeploymentItem( SAMPLE_DATA_FILE_PATH )]
        public void LoadsJsonData()
        {
            ////Setup
            //int expectedCount = 393;

            //string cwd = System.Environment.CurrentDirectory;
            //string dataFilePath = Path.Combine( cwd, SAMPLE_TANK_DATA );
            //Assert.IsTrue( File.Exists( dataFilePath ), "Setup error. Check that file deployment is setup." );

            //string jsonData = File.ReadAllText( dataFilePath );

            //TanksInfo ti = new TanksInfo();

            ////Test
            //bool result = ti.Load( jsonData );

            ////Validate
            //if( null != ti.TrappedError )
            //{
            //    Console.WriteLine( ti.TrappedError.ToString() );
            //}
            //Assert.IsTrue( result, "Failed to load the test data file content." );
            //int actualCount = ti.Tanks.Count;
            //Assert.IsTrue( ( expectedCount == actualCount ), string.Format( "Count mismatch. Expected {0} but have {1} members.", expectedCount, actualCount ) );
        }
    #endregion LoadsJsonData

    #region GetsClanWarsTanks
        [TestMethod]
        [DeploymentItem( SAMPLE_DATA_FILE_PATH )]
        public void GetsClanWarsTanks()
        {
            ////Setup
            //int expectedCount = 43;

            //string cwd = System.Environment.CurrentDirectory;
            //string dataFilePath = Path.Combine( cwd, SAMPLE_TANK_DATA );
            //Assert.IsTrue( File.Exists( dataFilePath ), "Setup error. Check that file deployment is setup." );

            //string jsonData = File.ReadAllText( dataFilePath );

            //TanksInfo ti = new TanksInfo();
            //bool result = ti.Load( jsonData );
            //if( null != ti.TrappedError )
            //{
            //    Console.WriteLine( ti.TrappedError.ToString() );
            //}
            //Assert.IsTrue( result, "Failed to load the test data file content." );

            ////Test
            //List<TankInfo> cwTanks = ti.GetClanWarsTanks();

            ////Validate
            //int actualCount = cwTanks.Count;
            //Assert.IsTrue( ( expectedCount == actualCount ), string.Format( "Count mismatch. Expected {0} but have {1} members.", expectedCount, actualCount ) );

            //foreach( TankInfo t in cwTanks )
            //{
            //    Console.WriteLine( "ID: {0}, Country: {1}, Tier: {2}, Type: {3}, Name: {4}", t.TankID, t.Nation, t.Tier, t.TankType, t.Name );
            //}
        }
    #endregion GetsClanWarsTanks

#endregion Test Methods

    }
}
