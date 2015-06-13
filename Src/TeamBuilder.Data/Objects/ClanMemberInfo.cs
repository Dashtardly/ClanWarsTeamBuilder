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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace WoT.Contributed.TeamBuilder.Data
{
    /// <summary>Container class for individual players (members) in a Clan.</summary>
    public class ClanMemberInfo
    {

#region Data


#endregion Data

#region Constructors

        /// <summary>Default constructor</summary>
        public ClanMemberInfo()
        {
            AccountID = 0;
            Name = string.Empty;
            Role = string.Empty;
        }

        /// <summary>Constructor. Creates and initializes an instance.</summary>
        /// <param name="omember">An object that contains the data about the member.</param>
        public ClanMemberInfo( object omember )
        {
            Dictionary<string,object> member = omember as Dictionary<string, object>;
            if( null == member ) throw new ArgumentException( "Unexpected type. Have: " + omember.GetType().ToString() );

            AccountID = ( int )member[ Globals.FIELD_ACCOUNT_ID   ];
            Name      =        member[ Globals.FIELD_ACCOUNT_NAME ] as string;
            Role      =        member[ Globals.FIELD_ROLE         ] as string;
        }

#endregion Constructors

#region Properties

        /// <summary>The unique ID number for the game player (member of the clan).</summary>
        public int    AccountID { get; set; }

        /// <summary>The player's in-game "Name"</summary>
        public string Name       { get; set; }

        /// <summary>The role the player has in the clan.</summary>
        /// <remarks>Roles are assigned by Clan leaders to identify players either by seniority or function they perform.</remarks>
        public string Role       { get; set; }

        public PlayerTanks Tanks { get; set; }

#endregion Properties

    }
}
