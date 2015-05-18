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

namespace WoT.Contributed.TeamBuilder
{
    /// <summary>Container class for the tanks (vehicles) that an individual player has acquired and played games in.</summary>
    public class PlayerTankInfo
    {

#region Data

        /// <summary>The list of response fields to be used in the API query</summary>
        public const string ApiQueryFieldsMemberInfo = "tank_id,statistics.battles,statistics.wins";

        //The field names returned from the Wargaming API call for information about a player's tanks.
        private const string FIELD_ID         = "tank_id";
        private const string FIELD_STATISTICS = "statistics";
        private const string FIELD_WINS       = "wins";
        private const string FIELD_BATTLES    = "battles";

#endregion Data

#region Constructors

        /// <summary>Constructor. Creates and initializes an instance.</summary>
        /// <param name="otank">An object that contains the data about a member's tank.</param>
        public PlayerTankInfo( object otank )
        {
            Dictionary<string,object> tank = otank as Dictionary<string, object>;
            if( null == tank ) throw new ArgumentException( "Unexpected type. Have: " + otank.GetType().ToString() );

            TankID  = ( int )tank[ FIELD_ID ];

            Dictionary<string,object> stats = tank[ FIELD_STATISTICS ] as Dictionary<string, object>;
            if( null == stats ) throw new ArgumentException( "Unexpected type. Have: " + tank[ FIELD_STATISTICS ].GetType().ToString() );

            Battles = ( int )stats[ FIELD_BATTLES ];
            Wins    = ( int )stats[ FIELD_WINS ];
        }

#endregion Constructors

#region Properties

        /// <summary>The number of games in the <see cref="TankID"/> that the player has played in.</summary>
        public int    Battles { get; set; }

        /// <summary>The unique ID number for the tank (vehicle) that the statistics apply to.</summary>
        public int    TankID  { get; set; }

        /// <summary>The number of games in the <see cref="TankID"/> that the player played in and won the game.</summary>
        public int    Wins    { get; set; }

        /// <summary>The percentage of games that the player has won in the <see cref="TankID"/> (values of 0.0 - 100.0).</summary>
        public float  WinRate  
        {
            get
            {
                float rate = ( 0 == Battles ) ? 0.0f : 100.0f * ( ( float )Wins / ( float )Battles );
                return rate;
            }
        }


#endregion Properties

    }
}
