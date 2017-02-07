using System;
using System.Net.Http;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace DotaApiManage.MatchDetails
{
    /// <summary>
    /// Match Detail api access
    /// </summary>
    public class ApiAccess
    {
        static HttpClient client = new HttpClient();
        private BaseResultSet store;
        // api key
        private string key = "80D9261FF631DE1AE99CB5179E69FF45";

        private string matchID = "";

        /// <summary>
        /// constructor for init of match retrieve
        /// </summary>
        /// <param name="mID">Match ID</param>
        public ApiAccess(string mID)
        {
            matchID = mID;
            // get the information from the api
            string response = GetApiResponse();
            // turn the given json into an object
            store = JsonConvert.DeserializeObject<BaseResultSet>(response);
        }

        /// <summary>
        /// calls the dota 2 web api and returns the given json
        /// </summary>
        /// <returns>
        /// string contaning json
        /// </returns>
        private string GetApiResponse()
        {
            // set up the api call
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.steampowered.com/IDOTA2Match_570/GetMatchDetails/V001/?key="+key+"&match_id="+matchID);
            request.Method = "GET";
            request.ContentType = "application/json";

            try
            {
                WebResponse webResponse = request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                string response = responseReader.ReadToEnd();
                // Console.WriteLine(response);
                responseReader.Close();
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine("Fail: {0}", e.Message);
                return "";
            }
        }

        /// <summary>
        /// Get method for match ID
        /// </summary>
        /// <returns>string</returns>
        public string GetMatchId()
        {
            return store.result.match_id;
        }

        /// <summary>
        /// Returns the result of the match (true if radiant won)
        /// </summary>
        /// <returns>Won</returns>
        public bool Result()
        {
            return store.result.radiant_win;
        }
        
        /// <summary>
        /// Returns if the player given was on the winning team
        /// </summary>
        /// <param name="playerid">Player ID to compare</param>
        /// <returns>Won</returns>
        public bool PlayerResult(string playerid)
        {
            try
            {
                var query = store.result.players.First(Player => Player.account_id == playerid);
                return (Result() && query.player_slot < 50) || (!Result() && query.player_slot > 50);
            }
            catch(ArgumentNullException e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        /// <summary>
        /// Returns the hero id of the hero played by the given id
        /// </summary>
        /// <param name="playerid">player id</param>
        /// <returns>int hero id</returns>
        public int HeroPlayed(string playerid)
        {
            try
            {
                var query = store.result.players.First(Player => Player.account_id == playerid);
                return query.hero_id;
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e);
                return 0;
            }
        }

        /// <summary>
        /// Returns the duration of the match in seconds
        /// </summary>
        /// <returns>int duration in seconds</returns>
        public int GetDuration()
        {
            return store.result.duration;
        }

        /// <summary>
        /// Returns kills by a given player
        /// </summary>
        /// <param name="playerid">player id</param>
        /// <returns>int of kills</returns>
        public int GetKillsByPlayer(string playerid)
        {
            try
            {
                var query = store.result.players.First(Player => Player.account_id == playerid);
                return query.kills;
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e);
                return 0;
            }
        }

        /// <summary>
        /// Returns deaths by a given player
        /// </summary>
        /// <param name="playerid">player id</param>
        /// <returns>int of deaths</returns>
        public int GetDeathsByPlayer(string playerid)
        {
            try
            {
                var query = store.result.players.First(Player => Player.account_id == playerid);
                return query.deaths;
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e);
                return 0;
            }
        }

        /// <summary>
        /// Returns assists by a given player
        /// </summary>
        /// <param name="playerid">player id</param>
        /// <returns>int of assists</returns>
        public int GetAssistsByPlayer(string playerid)
        {
            try
            {
                var query = store.result.players.First(Player => Player.account_id == playerid);
                return query.assists;
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e);
                return 0;
            }
        }

    }
}
