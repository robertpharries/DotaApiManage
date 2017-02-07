using System;
using System.Net.Http;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DotaApiManage.MatchHistory
{
    /// <summary>
    /// Match History api access
    /// </summary>
    public class ApiAccess
    {
        static HttpClient client = new HttpClient();
        static BaseResultSet store;
        // api key
        private string key = "80D9261FF631DE1AE99CB5179E69FF45";

        private string accountID = "";
        private string matches = "";

        /// <summary>
        /// constructor for init of history retrieve
        /// </summary>
        /// <param name="aID">Account ID</param>
        /// <param name="m">Matches to get</param>
        public ApiAccess(string aID, string m)
        {
            accountID = aID;
            matches = m;
            // get the information from the api
            string response = GetApiResponse();
            // turn the given json into an object
            store = JsonConvert.DeserializeObject<BaseResultSet>(response);
        }

        /// <summary>
        /// constructor for init of history retrieve
        /// </summary>
        /// <param name="aID">Account ID</param>
        public ApiAccess(string aID)
        {
            accountID = aID;
            matches = "25";
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
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/V001/?key="+key+"&account_id="+accountID+"&matches_requested="+matches);
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
        /// Gets the match id's retrieved
        /// </summary>
        /// <returns>string list containing match id's</returns>
        public List<string> GetMatchIds()
        {
            List<string> matchids = new List<string>();

            foreach(var match in store.result.matches)
            {
                matchids.Add(match.match_id);
            }

            return matchids;
        }
    }
}
