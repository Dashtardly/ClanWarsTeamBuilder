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

using System.Web.Script.Serialization;  //for JavaScriptSerializer. Requires reference to System.Web.Extensions

#endregion

namespace WoT.Contributed.TeamBuilder.Data
{
    /// <summary>Container class for information about the tank vehicles in the game.</summary>
    public class Tanks
    {

#region Data

        /// <summary>The list of response fields to be used in the API query</summary>
        public const string ApiQueryFieldsTanksInfo = "tank_id,name,short_name_i18n,level,type,nation_i18n";

        public const int MAX_TIER = 10;


        /*
         * JSON DATA EXAMPLE:
            {
                "status": "ok",
                "meta": {
                    "count": 396
                },
                "data": {
                    "1": {
                        "nation_i18n": "U.S.S.R.",
                        "name": "#ussr_vehicles:T-34",
                        "level": 5,
                        "short_name_i18n": "T-34",
                        "type": "mediumTank",
                        "tank_id": 1
                    },
                    "33": {
                        "nation_i18n": "U.S.A.",
                        "name": "#usa_vehicles:T14",
                        "level": 5,
                        "short_name_i18n": "T14",
                        "type": "heavyTank",
                        "tank_id": 33
                    },
                    ...        
         */


#endregion Data

#region Constructors

        /// <summary>Default constructor</summary>
        public Tanks()
        {
            AllTanks = new List<TankInfo>();
        }

#endregion Constructors

#region Properties

        /// <summary>The list of tanks (game vehicles) that games can be played in.</summary>
        public List<TankInfo> AllTanks      { get; private set; }

        /// <summary>The list of tanks (game vehicles) that are preferred for use in the game mode "Clan Wars".</summary>
        public List<TankInfo> ClanWarsTanks { get; private set; }

        /// <summary>The error encountered while loading (if any).</summary>
        public Exception      TrappedError  { get; private set; }

#endregion Properties

#region Public Methods

    #region GetIfClanWarsTank
        ///====================================================================
        /// <summary>The list of tanks (game vehicles) that are considered usable in Clan Wars battles.</summary>
        /// <returns>The list of tanks that are "usable" in Clan Wars.</returns>
        /// <remarks>Typically this is just the top tier (10) tanks however, the USA
        ///          Artillery M53/55 is preferred in some circumstances due to the
        ///          turret and better mobility. Also, in some cases the new Tier 8
        ///          scouts (e.g., German Ru251) have also been used.</remarks>
        ///====================================================================
        public TankInfo GetIfClanWarsTank( int tankID ) 
        {
            TankInfo tank = AllTanks.Find( t => t.TankID == tankID );
            bool result = ( null != tank );
            if( result )
            {
                result = IsClanWarsTank( tank );
            }
            TankInfo cwTank = ( result ) ? tank : null;
            return tank;
        }
    #endregion GetIfClanWarsTank


    #region IsClanWarsTank( int )
        ///====================================================================
        /// <summary>The list of tanks (game vehicles) that are considered usable in Clan Wars battles.</summary>
        /// <returns>The list of tanks that are "usable" in Clan Wars.</returns>
        /// <remarks>Typically this is just the top tier (10) tanks however, the USA
        ///          Artillery M53/55 is preferred in some circumstances due to the
        ///          turret and better mobility. Also, in some cases the new Tier 8
        ///          scouts (e.g., German Ru251) have also been used.</remarks>
        ///====================================================================
        public bool IsClanWarsTank( int tankID ) 
        {

            TankInfo tank = AllTanks.Find( t => t.TankID == tankID );
            bool result = ( null != tank );
            if( result )
            {
                result = IsClanWarsTank( tank );
            }
            return result;
        }
    #endregion IsClanWarsTank( int )

    #region IsClanWarsTank( TankInfo )
        ///====================================================================
        /// <summary>The list of tanks (game vehicles) that are considered usable in Clan Wars battles.</summary>
        /// <returns>The list of tanks that are "usable" in Clan Wars.</returns>
        /// <remarks>Typically this is just the top tier (10) tanks however, the USA
        ///          Artillery M53/55 is preferred in some circumstances due to the
        ///          turret and better mobility. Also, in some cases the new Tier 8
        ///          scouts (e.g., German Ru251) have also been used.</remarks>
        ///====================================================================
        public static bool IsClanWarsTank( TankInfo t ) 
        {
            bool result = ( IsTopTierTank(       t ) ) ||
                          ( IsClanWarsArtillery( t ) ) ||
                          ( IsClanWarsScout(     t ) );
            return result;
        }
    #endregion IsClanWarsTank( TankInfo )

#region IsClanWarsXXX

        /// <summary>Utility exception method to include a lower tiered tank in the list of clan wars tanks.</summary>
        /// <param name="tank">The tank info to evaluation</param>
        /// <returns></returns>
        public static bool IsClanWarsArtillery( TankInfo tank )
        {
            return ( tank.Tier ==  ( MAX_TIER - 1 ) ) && ( tank.TankType == TankInfo.TypeArtillery ) && ( tank.Name == "M53_M55" );
        }

        /// <summary>Utility exception method to include a lower tiered "scout" tank in the list of clan wars tanks.</summary>
        /// <param name="tank">The tank info to evaluation</param>
        public static bool IsClanWarsScout( TankInfo x )
        {
            return ( x.Tier ==  ( MAX_TIER - 2 ) ) && ( x.TankType == TankInfo.TypeLightTank );
        }

        /// <summary>Utility exception method to include a lower tiered tank in the list of clan wars tanks.</summary>
        /// <param name="tank">The tank info to evaluation</param>
        public static bool IsTopTierTank( TankInfo tank )
        {
            return ( tank.Tier == MAX_TIER ); 
        }

    #endregion IsClanWarsXXX

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
                    Dictionary<string,object> tankData = d[ "data" ] as Dictionary<string, object>; ;

                    foreach( string key in tankData.Keys )
                    {
                        TankInfo tank = new TankInfo( tankData[ key ] );
                        AllTanks.Add( tank );
                    }
                }
                catch( Exception ex )
                {
                    TrappedError = ex;
                }
                result = ( 0 < AllTanks.Count );
            }
            return result;
        } 
        #endregion

#endregion Public Methods

#region Private Methods
#endregion Private Methods

    }
}
