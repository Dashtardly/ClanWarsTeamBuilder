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

#endregion

namespace WoT.Contributed.TeamBuilder
{
    /// <summary>Container class for the tanks (vehicles) that an individual player has acquired and played games in.</summary>
    public class TankInfo
    {

#region Data

        /// <summary>The list of response fields to be used in the API query</summary>
        public const string ApiQueryFieldsMemberInfo = "name,level,tank_id,type,nation";

        public const string TypeTankDestroyer = "Tank Destroyer";
        public const string TypeHeavyTank     = "Heavy Tank";
        public const string TypeLightTank     = "Light Tank";
        public const string TypeMediumTank    = "Medium Tank";
        public const string TypeArtillery     = "Artillery";

        //The field names returned from the Wargaming API call for information about a tank.
        private const string FIELD_ID     = "tank_id";
        private const string FIELD_LEVEL  = "level";
        private const string FIELD_NAME   = "name";
        private const string FIELD_NATION = "nation";
        private const string FIELD_TYPE   = "type";

        /// <summary>Map of WoT game tank types to friendlier names.</summary>
        private Dictionary<string,string> TypeToTankType = new Dictionary<string, string>()
        {
            {"AT-SPG",     TypeTankDestroyer},
            {"heavyTank",  TypeHeavyTank    },
            {"lightTank",  TypeLightTank    },
            {"mediumTank", TypeMediumTank   },
            {"SPG",        TypeArtillery    },
        };

#endregion Data

#region Constructors

        /// <summary>Constructor. Creates and initializes an instance.</summary>
        /// <param name="otank">An object that contains the data about a member's tank.</param>
        public TankInfo( object otank )
        {
            Dictionary<string,object> tank = otank as Dictionary<string, object>;
            if( null == tank ) throw new ArgumentException( "Unexpected type. Have: " + otank.GetType().ToString() );

            TankID   =       ( int )tank[ FIELD_ID     ];
            Name     = GetTankName( tank[ FIELD_NAME   ] as string );
            Nation   =              tank[ FIELD_NATION ] as string;
            TankType = GetTankType( tank[ FIELD_TYPE   ] as string );
            Tier     =       ( int )tank[ FIELD_LEVEL  ];
        }

#endregion Constructors

#region Properties

        /// <summary>A friendly name for the vehicle (e.g., "T-62A").</summary>
        public string Name { get; set; }

        /// <summary>The name of the country that has the tank (e.g., "ussr").</summary>
        public string Nation { get; set; }

        /// <summary>The unique ID number for the tank (vehicle).</summary>
        public int    TankID  { get; set; }

        /// <summary>The type of tank that <see cref="TankID"/> is.</summary>
        /// <remarks>A generic category name for the tank that implies vehicle characteristics.</remarks>
        public string TankType { get; set; }

        /// <summary>A number (1-10) that describes the vehicle technology level.</summary>
        /// <remarks>The API calls this "level" but in-game is known as "Tier".</remarks>
        public int    Tier    { get; set; }


#endregion Properties

#region Private Methods

    #region GetTankName
        ///====================================================================
        /// <summary>Generates a friendlier name from the API tank name.</summary>
        /// <param name="wotName">The tank name to convert.</param>
        /// <returns>A friendlier name to use (if found) otherwise the full name.</returns>
        /// <remarks>Turns "#ussr_vehicles:T-34" into "T-34".</remarks>
        ///====================================================================
        private string GetTankName( string wotName )
        {
            string tankName = wotName;
            string[] pieces = wotName.Split( new char[]{':'} );
            if( 2 == pieces.Length )
            {
                tankName = pieces[ 1 ];
            }
            return tankName;
        }
    #endregion GetTankName

    #region GetTankType
        ///====================================================================
        /// <summary>Translates an API tank type classifications into a friendlier name.</summary>
        /// <param name="wotType">The tank type to lookup.</param>
        /// <returns>A friendlier name to use (if found) otherwise the given type name.</returns>
        ///====================================================================
        private string GetTankType( string wotType )
        {
            string tankType = wotType;
            if( TypeToTankType.Keys.Contains<string>( wotType ) )
            {
                tankType = TypeToTankType[ wotType ];
            }
            return tankType;
        }
    #endregion GetTankType

#endregion Private Methods

    }
}
