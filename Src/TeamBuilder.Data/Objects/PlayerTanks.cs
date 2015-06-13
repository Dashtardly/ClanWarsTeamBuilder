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

using System.Web.Script.Serialization;  //for JavaScriptSerializer. Requires reference to System.Web.Extensions

#endregion

namespace WoT.Contributed.TeamBuilder.Data
{
    /// <summary>Container class for information about a player in the game and their performance in clan war's usable tanks.</summary>
    public class PlayerTanks
    {

#region Data


#endregion Data

#region Constructors

        /// <summary>Default constructor</summary>
        public PlayerTanks()
        {
            ClanWarsTanks = new List<TankInfo>();
            TanksOwned    = new List<int>();
        }

#endregion Constructors

#region Properties

        /// <summary>The unique account ID number for the player.</summary>
        public int            AccountID     { get; set; }

        /// <summary>The list of tanks (game vehicles) that the <see cref="PlayerID"/> owns and has played a game in.</summary>
        public List<int>      TanksOwned    { get; private set; }

        /// <summary>The list of tanks (game vehicles) that the <see cref="PlayerID"/> owns and has played a game in.</summary>
        public List<TankInfo> ClanWarsTanks { get; private set; }


        /// <summary>The error encountered while loading (if any).</summary>
        public Exception      TrappedError  { get; private set; }

#endregion Properties

#region Public Methods

    #region LoadTanksOwned
        ///====================================================================
        /// <summary>Initializes the state from a set of JSON data.</summary>
        /// <param name="jsonData">The data to use.</param>
        /// <returns>True if the data was loaded successfully. If false, check
        ///          the <see cref="TrappedError"/> property.</returns>
        ///====================================================================
        public bool LoadTanksOwned( string jsonData )
        {
            bool result = false;
            TanksOwned.Clear();
            if( !string.IsNullOrEmpty( jsonData ) )
            {
                try
                {

                    #region JSON DATA EXAMPLE

                    /*
                        {
                        "status": "ok",
                        "meta": {
                            "count": 1
                        },
                        "data": {
                            "1000029478": [
                                {
                                    "tank_id": 2849
                                },
                            ...        
                     */

                    
                    #endregion                    var js = new JavaScriptSerializer();

                    var js = new JavaScriptSerializer();
                    var d = js.Deserialize<dynamic>( jsonData );

                    string s  = d[ "status" ];
                    result = ( "ok" == s );
                    if( result )
                    {
                        dynamic[] tankData = d[ "data" ][ AccountID.ToString() ];
                        for( int i = 0; i < tankData.Length; i++ )
                        {
                            int tankID  = ( int )tankData[ i ][ Globals.FIELD_TANK_ID ];
                            TanksOwned.Add( tankID );
                            result = true;
                        }
                    }
                }
                catch( Exception ex )
                {
                    TrappedError = ex;
                    result = false;
                }
            }
            return result;
        } 
    #endregion LoadTanksOwned

    #region LoadClanWarsTanks
        ///====================================================================
        /// <summary>Initializes the state from a set of JSON data.</summary>
        /// <param name="jsonData">The data to use.</param>
        /// <returns>True if the data was loaded successfully. If false, check
        ///          the <see cref="TrappedError"/> property.</returns>
        ///====================================================================
        public bool LoadClanWarsTanks( Tanks tanks )
        {
            bool result = false;
            ClanWarsTanks.Clear();
            foreach( int tankID in TanksOwned )
            {
                TankInfo tank = tanks.GetIfClanWarsTank( tankID );
                if( null != tank )
                {
                    ClanWarsTanks.Add( tank.Copy() );
                    result = true;
                }
            }

            return result;
        } 
    #endregion LoadClanWarsTanks

    #region LoadTankPerformance
        ///====================================================================
        /// <summary>Initializes the performance data for the player's tanks
        ///          from a set of JSON data.</summary>
        /// <param name="jsonData">The data to use.</param>
        /// <returns>True if the data was loaded successfully. If false, check
        ///          the <see cref="TrappedError"/> property.</returns>
        ///====================================================================
        public bool LoadTankPerformance( string jsonData )
        {
            bool result = false;
            if( !string.IsNullOrEmpty( jsonData ) )
            {
                try
                {
                    var js = new JavaScriptSerializer();
                    var d = js.Deserialize<dynamic>( jsonData );

                    string s  = d[ "status" ];
                    result = ( "ok" == s );
                    if( result )
                    {
                        dynamic[] data = d[ "data" ][ AccountID.ToString() ];
                        foreach( TankInfo tank in ClanWarsTanks )
                        {
                            tank.Performance = FindPerformanceData( data, tank.TankID );
                        }
                    }
                }
                catch( Exception ex )
                {
                    TrappedError = ex;
                }
            }
            return result;
        }
    #endregion LoadTankPerformance

#endregion Public Methods

#region Private Methods

    #region FindPerformanceData
        ///====================================================================
        /// <summary>Finds the peformance data for the desired tank.</summary>
        /// <param name="data">The performance data to look in.</param>
        /// <param name="tankID">The tank the performance is required for.</param>
        /// <returns>The performance data for the tank, null if none found.</returns>
        ///====================================================================
        private PerformanceInfo FindPerformanceData( dynamic[] data, int tankID )
        {
            PerformanceInfo p = null;
            foreach( Dictionary<string,object> performanceData in data )
            {
                if( PerformanceInfo.IsMatch( performanceData, tankID ) )
                {
                    p = new PerformanceInfo( performanceData );
                    break;
                }
            }
            return p;
        }
    #endregion FindPerformanceData

#endregion Private Methods

    }
}
