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
    /// <summary>Fake of <see cref="ApiQuery"/> class to allow unit testing with "fixed" data.</summary>
    public class ApiQueryFake : IApiQuery
    {

#region Data

        /// <summary>The assigned ID number for a registered WarGaming application.</summary>
        private const string APPLICATION_ID = "f539b9afae7d020157951f19cecf36ec";



        public Dictionary<string,string> ClanCache        = new Dictionary<string, string>();

        public Dictionary<string,string> PerformanceCache = new Dictionary<string, string>();

        public Dictionary<string,string> PlayerTanksCache = new Dictionary<string, string>();

        public Dictionary<string,string> TanksCache       = new Dictionary<string, string>();

#endregion Data

#region Properties

        /// <summary>The path to the directory where the cached data is stored.</summary>
        public string CacheDirectoryPath { get; set; }

#endregion Properties

#region Public Methods

        public string GetClanInfo( string appID, string clanID )
        {
            string query = string.Format( Globals.FORMAT_QUERY_CLAN, appID, Globals.QueryFieldsClan, clanID );
            string response = DownloadAndCache( query, ClanCache, clanID );
            return response;
        }

        public string GetTanksOwned( string appID, string accountID )
        {
            string query = string.Format( Globals.FORMAT_QUERY_PLAYERTANKS, appID, Globals.QueryFieldsPlayersTanks, accountID );
            string response = DownloadAndCache( query, PlayerTanksCache, accountID );
            return response;
        }

        public string GetTanksInfo( string appID )
        {
            string query = string.Format( Globals.FORMAT_QUERY_TANKS, appID, Globals.QueryFieldsTanks );
            string response = DownloadAndCache( query, PlayerTanksCache, "Tanks" );
            return response;
        }

        public string GetPerformanceInfo( string appID, string accountID )
        {
            string query = string.Format( Globals.FORMAT_QUERY_PERFORMANCE, appID, Globals.QueryFieldsPerformance, accountID );
            string response = DownloadAndCache( query, PerformanceCache, accountID );
            return response;
        }

        public void LoadCachedData()
        {
            //TODO
        }

        public bool SaveCachedData()
        {
            //TODO
            return false;
        }
#endregion Public Methods


#region Private Methods

    #region DownloadAndCache
        ///====================================================================
        /// <summary>Returns the data that has been set into the cache, emulating
        ///          either data obtained from a web call or previously cached data.</summary>
        /// <param name="query">The query to use.</param>
        /// <param name="cache">The cache to store the response into.</param>
        /// <param name="key">The unique ID value to store the response under.</param>
        /// <returns>The response obtained. Empty string if there was a problem.</returns>
        ///====================================================================
        private string DownloadAndCache( string query, Dictionary<string, string> cache, string key )
        {
            string response = string.Empty;
            if( cache.Keys.Contains<string>( key ) )
            {
                response = cache[ key ];
            }
            else
            {
                throw new Exception( query );
            }
            return response;
        }
    #endregion DownloadAndCache

#endregion Private Methods




    }
}
