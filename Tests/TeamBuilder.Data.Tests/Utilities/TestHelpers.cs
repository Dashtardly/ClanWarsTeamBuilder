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
// 2015-06-13  M. Lansdaal    n/a        Initial version.
//

#endregion

#region Using Directives

using System;
using System.Collections.Generic;
using System.IO;    //for File, Path
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting; //For Assert

#endregion

namespace TeamBuilder.Data.Tests
{
    internal class TestHelpers
    {

    #region ReadFromFile
        ///====================================================================
        /// <summary>Utility method to read the contents of <paramref name="fileName"/>.</summary>
        /// <returns>The contents of the file.</returns>
        ///====================================================================
        internal static string ReadFromFile( string fileName )
        {
            string cwd = System.Environment.CurrentDirectory;
            string dataFilePath = Path.Combine( cwd, fileName );
            Assert.IsTrue( File.Exists( dataFilePath ), "Setup error. Check that file deployment is setup." );

            string contents = File.ReadAllText( dataFilePath );
            return contents;
        }
    #endregion ReadFromFile

    #region WriteToFile
        ///====================================================================
        /// <summary>Utility method to dump the <paramref name="contents"/> to a random file.</summary>
        /// <param name="contents">The data to put into the file.</param>
        ///====================================================================
        internal static void WriteToFile( string contents )
        {
            string cwd = System.Environment.CurrentDirectory;
            string fileName = Path.GetTempFileName();
            string filePath = Path.Combine( cwd, fileName );
            Console.WriteLine( "WriteToFile. Path: " + filePath );
            File.WriteAllText( filePath, contents );
        }
    #endregion WriteToFile

    }

}
