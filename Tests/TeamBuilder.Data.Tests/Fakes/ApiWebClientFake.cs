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

using WoT.Contributed.TeamBuilder.Data;

#endregion

namespace TeamBuilder.Data.Tests
{
    internal class ApiWebClientFake : IApiWebClient
    {

        /// <summary>Internal store of registered queries and the response to provide.</summary>
        private Dictionary< string, object > ResponseToQuery = new Dictionary<string, object>();

    #region Properties

        internal string QueryResponse { get; set; }

        internal Exception QueryException { get; set; }

    #endregion Properties

#region Public Interface

        public string DownloadString( string query )
        {
            if( ResponseToQuery.Keys.Contains<string>( query ) )
            {
                object response = ResponseToQuery[ query ];
                if( response is Exception )
                {
                    throw response as Exception;
                }
                return response as string;
            }
            if( null != QueryException )
            {
                throw QueryException;
            }
            return QueryResponse;
        }

#endregion Public Interface

#region Internal Methods

    #region RegisterResponse
        ///====================================================================
        /// <summary>Registers what the reponse will be for the specified <paramref name="query"/>.</summary>
        /// <param name="query">The query to use.</param>
        /// <param name="response">The desired response (either text content or an exception).</param>
        ///====================================================================
        internal void RegisterResponse( string query, object response )
        {
            if( !ResponseToQuery.Keys.Contains<string>( query ) )
            {
                ResponseToQuery.Add( query, response );
            }
            else
            {
                ResponseToQuery[ query ] = response;
            }
        }
    #endregion RegisterResponse

#endregion Internal Methods

    }
}
