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
using System.Linq;
using System.Net;   //for WebClient
using System.Text;
using System.Threading.Tasks;

#endregion

namespace WoT.Contributed.TeamBuilder.Data
{
    public class ApiWebClient : IApiWebClient
    {

        ///====================================================================
        /// <summary>Performs a live web client access to download the data for
        ///          the specified <paramref name="query"/>.</summary>
        /// <param name="query">The url query to execute.</param>
        /// <remarks>No validation of the <paramref name="query"/> other than to
        ///          make sure that it is not null/empty.</remarks>
        /// <exception cref="ArgumentException">Thrown if <paramref name="query"/> is not set.</exception>
        ///====================================================================
        public string DownloadString( string query )
        {
            string response = string.Empty;
            if( string.IsNullOrEmpty( query ) )
            {
                throw new ArgumentException( "parameter is null or empty" );
            }
            else
            {
                using( WebClient wc = new WebClient() )
                {
                    response = wc.DownloadString( query );
                }
            }
            return response;
        }

    }
}
