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
    public class Globals
    {

        /// <summary>URL Format string to be used when building a query string to find out the membership of a clan.</summary>
        public const string FORMAT_QUERY_CLAN = "https://api.worldoftanks.com/wgn/clans/info/?application_id={0}&language=en&fields={1}&clan_id={2}";

        /// <summary>URL Format string to be used when building a query string to find the performance for a player in some/all of their of tanks.</summary>
        public const string FORMAT_QUERY_PERFORMANCE = "http://api.worldoftanks.com/wot/tanks/stats/?application_id={0}&language=en&fields={1}&account_id={2}&tank_id={3}";
        
        /// <summary>URL Format string to be used when building a query string to find the list of tanks owned by a player.</summary>
        public const string FORMAT_QUERY_PLAYERTANKS = "https://api.worldoftanks.com/wot/account/tanks/?application_id={0}&language=en&fields={1}&account_id={2}";

        /// <summary>URL Format string to be used when building a query string to find information about the tanks in the game.</summary>
        public const string FORMAT_QUERY_TANKS = "https://api.worldoftanks.com/wot/encyclopedia/tanks/?application_id={0}&language=en&fields={1}";



        /// <summary>The list of fields to be found in the response to an API query to get information about a clan's members.</summary>
        public const string QueryFieldsClan = "members.account_id,members.account_name,members.role";

        /// <summary>The list of fields to be found in the response to an API query to get the performance information for a player's tanks.</summary>
        public const string QueryFieldsPerformance = "account_id,tank_id,all.battles,all.damage_dealt,all.hits_percents,all.wins";

        /// <summary>The list of fields to be found in the response to an API query to get information about a player's tanks.</summary>
        public const string QueryFieldsPlayersTanks = "tank_id";

        /// <summary>The list of fields to be found in the response to an API query to get information about the tanks in the game.</summary>
        public const string QueryFieldsTanks = "name,short_name_i18n,level,tank_id,type,nation_i18n";


        //The field names returned from the Wargaming API call for information about a player's tanks.
        public const string FIELD_ACCOUNT_ID   = "account_id";
        public const string FIELD_ACCOUNT_NAME = "account_name";
        public const string FIELD_ALL          = "all";
        public const string FIELD_BATTLES      = "battles";
        public const string FIELD_DAMAGE       = "damage_dealt";
        public const string FIELD_HITS         = "hits_percents";
        public const string FIELD_LEVEL        = "level";
        public const string FIELD_NAME         = "name";
        public const string FIELD_NATION       = "nation_i18n";
        public const string FIELD_ROLE         = "role";
        public const string FIELD_SHORTNAME    = "short_name_i18n";
        public const string FIELD_TANK_ID      = "tank_id";
        public const string FIELD_TYPE         = "type";
        public const string FIELD_WINS         = "wins";


    }
}
