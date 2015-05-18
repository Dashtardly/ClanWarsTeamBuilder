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
// 2015-05-16  M. Lansdaal    n/a        Initial version.
//

#endregion

#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Script.Serialization;  //for JavaScriptSerializer. Requires reference to System.Web.Extensions

#endregion

namespace WoT.Contributed.TeamBuilder
{
    /// <summary>Container class for information about a player in the game.</summary>
    public class PlayerInfo
    {

#region Data

        /// <summary>The ID number for World of Tanks Player "ReaperJG26" on the North America server.</summary>
        /// <remarks>Is a member of clan F-3 and chosen only because his account was one of the first returned in the clan member query.</remarks>
        private const string PLAYER_ID_REAPERJG26 = "1000029478";

/*
 * JSON DATA EXAMPLE:
             {
                "status": "ok",
                "meta": {
                    "count": 1
                },
                "data": {
                    "1000029478": [
                        {
                            "statistics": {
                                "wins": 665,
                                "battles": 1248
                            },
                            "tank_id": 2849
                        },
                    ...        
 */

        //The field names returned from the Wargaming API call for information about a Clan.
        private const string FIELD_ID   = "account_id";
        private const string FIELD_NAME = "account_name";
        private const string FIELD_ROLE = "role";

#endregion Data

#region Constructors

        /// <summary>Default constructor</summary>
        public PlayerInfo()
        {
            PlayerID  = PLAYER_ID_REAPERJG26;
            Tanks = new List<PlayerTankInfo>();
        }

#endregion Constructors

#region Properties

        /// <summary>The unique ID number for the clan.</summary>
        public string               PlayerID     { get; set; }

        /// <summary>The list of tanks (game vehicles) that the <see cref="PlayerID"/> owns and has played a game in.</summary>
        public List<PlayerTankInfo> Tanks        { get; private set; }

        /// <summary>The role the player has in the clan.</summary>
        /// <remarks>Roles are assigned by Clan leaders to identify players either by seniority or function they perform.</remarks>
        public Exception            TrappedError { get; private set; }

#endregion Properties

#region Public Methods

    #region Load
        ///====================================================================
        /// <summary>Initializes the state from a set of JSON data.</summary>
        /// <param name="jsonData">The data to use.</param>
        /// <returns>True if the data was loaded successfully. If false, check
        ///          the <see cref="TrappedError"/> property.</returns>
        ///====================================================================
        public bool Load( string jsonData )
        {
            bool result = false;
            if( !string.IsNullOrEmpty( jsonData ) )
            {
                try
                {
                    var js = new JavaScriptSerializer();
                    var d = js.Deserialize<dynamic>( jsonData );

                    string s  = d[ "status" ];
                    var odata = d[ "data" ];
                    dynamic[] tankData = d[ "data" ][ PlayerID ];

                    for( int i = 0; i < tankData.Length; i++ )
                    {
                        PlayerTankInfo tank = new PlayerTankInfo( tankData[ i ] );
                        Tanks.Add( tank );
                    }
                }
                catch( Exception ex )
                {
                    TrappedError = ex;
                }
                result = ( 0 < Tanks.Count );
            }
            return result;
        } 
        #endregion

#endregion Public Methods

#region Private Methods
#endregion Private Methods

    }
}
