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
    /// <summary>Container class for information about a Clan.</summary>
    public class ClanInfo
    {

#region Data

        /// <summary>The ID number for World of Tanks Clan on the North America server named "[F-3] Fortuna Favet Fortibus"</summary>
        private const string CLAN_ID_F3 = "1000007884";

/*
 * JSON DATA EXAMPLE:
 {
	"status" : "ok",
	"meta" : {
		"count" : 1
	},
	"data" : {
		"1000007884" : {
			"members" : [{
					"role" : "combat_officer",
					"account_name" : "merlinsmagic",
					"account_id" : 1000024587
				}, {
					"role" : "executive_officer",
					"account_name" : "ReaperJG26",
					"account_id" : 1000029478
				}, {
                    ...        
 */

        //The field names returned from the Wargaming API call for information about a Clan.
        private const string FIELD_ID   = "account_id";
        private const string FIELD_NAME = "account_name";
        private const string FIELD_ROLE = "role";

#endregion Data

#region Constructors

        /// <summary>Default constructor</summary>
        public ClanInfo()
        {
            ClanID  = CLAN_ID_F3;
            Members = new List<ClanMemberInfo>();
        }

#endregion Constructors

#region Properties

        /// <summary>The unique ID number for the clan.</summary>
        public string               ClanID            { get; set; }

        /// <summary>The list of game player's that are members in the clan.</summary>
        public List<ClanMemberInfo> Members           { get; private set; }

        /// <summary>The role the player has in the clan.</summary>
        /// <remarks>Roles are assigned by Clan leaders to identify players either by seniority or function they perform.</remarks>
        public Exception            TrappedError      { get; private set; }

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
                    var clanData = d[ "data" ][ ClanID ];

                    dynamic[] members = clanData[ "members" ];
                    for( int i = 0; i < members.Length; i++ )
                    {
                        ClanMemberInfo member = new ClanMemberInfo( members[ i ] );
                        Members.Add( member );
                    }
                }
                catch( Exception ex )
                {
                    TrappedError = ex;
                }
                result = ( 0 < Members.Count );
            }
            return result;
        } 
        #endregion

#endregion Public Methods

#region Private Methods
#endregion Private Methods

    }
}
