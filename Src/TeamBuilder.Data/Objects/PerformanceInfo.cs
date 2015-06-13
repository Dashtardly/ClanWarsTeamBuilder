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
// 2015-05-22  M. Lansdaal    n/a        Initial version.
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
    /// <summary>Container class for a player's performance statistics in each tank (vehicle) that they have played games in.</summary>
    public class PerformanceInfo
    {

#region Data

        /*
            {
	            "status" : "ok",
	            "meta" : {
		            "count" : 1
	            },
	            "data" : {
		            "1000029478" : [{
				            "all" : {
					            "damage_dealt" : 2457215,
					            "wins" : 569,
					            "hits_percents" : 82,
					            "battles" : 1130
				            },
				            "account_id" : 1000029478,
				            "tank_id" : 14881
			            }, {
				            "all" : {
					            "damage_dealt" : 1772180,
					            "wins" : 666,
					            "hits_percents" : 78,
					            "battles" : 1251
				            },
				            "account_id" : 1000029478,
				            "tank_id" : 2849
			            }
		            ]
	            }
            }
         */


#endregion Data

#region Constructors

        /// <summary>Constructor. Creates and initializes an instance.</summary>
        /// <param name="tankPerfData">An dictionary containing performance data (by a player) in a specific tank.</param>
        public PerformanceInfo( Dictionary<string,object> tankPerfData )
        {

            TankID  = ( int )tankPerfData[ Globals.FIELD_TANK_ID ];

            Dictionary<string,object> allData = tankPerfData[ Globals.FIELD_ALL ] as Dictionary<string, object>;
            if( null == allData ) throw new ArgumentException( "Unexpected type. Have: " + tankPerfData[ Globals.FIELD_ALL ].GetType().ToString() );

            Battles     = ( int )allData[ Globals.FIELD_BATTLES ];
            Damage      = ( int )allData[ Globals.FIELD_DAMAGE ];
            HitsPercent = ( int )allData[ Globals.FIELD_HITS ];
            Wins        = ( int )allData[ Globals.FIELD_WINS ];
        }

#endregion Constructors

#region Properties

        /// <summary>The average of damage done with the <see cref="TankID"/>.</summary>
        public int AverageDamage
        {
            get
            {
                int avg = ( 0 == Battles ) ? 0 : Damage / Battles;
                return avg;
            }
        }

        /// <summary>The number of games that the player has played in using the <see cref="TankID"/>.</summary>
        public int    Battles { get; set; }

        /// <summary>The total amount of damage done to enemy vehicles using the tank <see cref="TankID"/>.</summary>
        public int    Damage { get; set; }

        /// <summary>The accuracy or percentage of shots taken that hit enemy vehicles using the tank <see cref="TankID"/> (values of 0 - 100).</summary>
        public int    HitsPercent { get; set; }

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

#region Public Methods

    #region IsMatch
        ///====================================================================
        /// <summary>Utility method to determine if the <paramref name="perfData"/> is for the specified <paramref name="tankID"/></summary>
        /// <param name="perfData">A performance data record for a specific tank (by ID).</param>
        /// <param name="tankID">The desired ID</param>
        /// <returns>True if the data is for the specified tank.</returns>
        ///====================================================================
        public static bool IsMatch( Dictionary<string, object> perfData, int tankID )
        {
            bool result = perfData.Keys.Contains<string>( Globals.FIELD_TANK_ID );
            if( result )
            {
                int id = ( int )perfData[ Globals.FIELD_TANK_ID ];
                result = ( id == tankID );
            }
            return result;
        }
    #endregion IsMatch

#endregion Public Methods


    }
}
