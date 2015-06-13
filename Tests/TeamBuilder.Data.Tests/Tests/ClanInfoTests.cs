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
    public class ClanInfoTests
    {

#region Data

        /// <summary>Sample API call response for information about a (specific) clan.</summary>
        private const string SAMPLE_CLAN_DATA = "WG_ClanInfo.json";

        /// <summary>Path to where the data file can be found so it can be deployed into the
        ///          directory being used for the unit test run.</summary>
        /// <remarks>In VS2010 the DeploymentItem file path was either aboslute or relative but
        ///          in VS2013 it is now relative to the build output directory.</remarks>
        private const string SAMPLE_DATA_FILE_PATH = "../../Data/" + SAMPLE_CLAN_DATA;

#endregion Data

#region Test Methods

    #region SampleDataFilePresent
        [TestMethod]
        [DeploymentItem( SAMPLE_DATA_FILE_PATH )]
        public void SampleDataFilePresent()
        {
            //Setup
            string cwd = System.Environment.CurrentDirectory;
            string dataFilePath = Path.Combine( cwd, SAMPLE_CLAN_DATA );

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
            //Setup
            int expectedCount = 100;    //Determined by content in the sample file.

            string cwd = System.Environment.CurrentDirectory;
            string dataFilePath = Path.Combine( cwd, SAMPLE_CLAN_DATA );
            Assert.IsTrue( File.Exists( dataFilePath ), "Setup error. Check that file deployment is setup." );

            string jsonData = File.ReadAllText( dataFilePath );

            ClanInfo ci = new ClanInfo();

            //Test
            bool result = ci.Load( jsonData );

            //Validate
            if( null != ci.TrappedError )
            {
                Console.WriteLine( ci.TrappedError.ToString() );
            }
            Assert.IsTrue( result, "Failed to load the test data file content." );
            int actualCount = ci.Members.Count;
            Assert.IsTrue( ( expectedCount == actualCount ), string.Format( "Count mismatch. Expected {0} but have {1} members.", expectedCount, actualCount ) );
        }
    #endregion LoadsJsonData

#endregion Test Methods

    }
}
