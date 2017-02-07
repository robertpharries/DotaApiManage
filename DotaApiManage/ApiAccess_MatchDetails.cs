using System;
using System.Net.Http;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace DotaApiManage.MatchDetails
{
    /*
     * Class for Accessing data from the Dota 2 api Match Details
     *
     * Loads based on parameters given at creation (Match id)
     */
    public class ApiAccess
    {
        static HttpClient client = new HttpClient();
        static BaseResultSet store;
        // api key
        private string key = "80D9261FF631DE1AE99CB5179E69FF45";

        private string matchID = "";

        // constructor provided with account id and matches
        public ApiAccess(string mID)
        {
            matchID = mID;
            // get the information from the api
            string response = GetApiResponse();
            // turn the given json into an object
            store = JsonConvert.DeserializeObject<BaseResultSet>(response);
        }

        // calls the dota 2 web api and returns the given json
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
        /// Returns the result of the match (true if radiant won)
        /// </summary>
        public bool result()
        {
            return store.result.radiant_win;
        }
        
        /// <summary>
        /// Returns if the player given was on the winning team
        /// </summary>
        /// <param name="playerid"></param>
        /// <returns></returns>
        public bool playerResult(string playerid)
        {
            try
            {
                var query = store.result.players.First(Player => Player.account_id == playerid);
                return (result() && query.player_slot < 50) || (!result() && query.player_slot > 50);
            }
            catch(ArgumentNullException e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
