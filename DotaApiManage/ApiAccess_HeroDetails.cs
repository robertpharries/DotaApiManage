﻿using System;
using System.Net.Http;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace DotaApiManage.Heroes
{
    public class HeroDetails
    {
        static HttpClient client = new HttpClient();
        static BaseResultSet store = null;

        private string key = "80D9261FF631DE1AE99CB5179E69FF45";

        /// <summary>
        /// constructor for init of hero detail retrieve
        /// </summary>
        public HeroDetails()
        {
            if (store == null)
            {
                // get the information from the api
                string response = GetApiResponse();
                // turn the given json into an object
                store = JsonConvert.DeserializeObject<BaseResultSet>(response);
            }
        }

        /// <summary>
        /// calls the hero details store and returns the given json
        /// </summary>
        /// <returns>
        /// string contaning json
        /// </returns>
        private string GetApiResponse()
        {
            // set up the api call
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.steampowered.com//IEconDOTA2_570/GetHeroes/V001/?key="+key+ "&language=english");
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
        /// Gets the name of a hero, given the hero_id
        /// </summary>
        /// <param name="id">id of the hero</param>
        /// <returns>string name</returns>
        public string GetHeroNameById(int id)
        {
            var query = store.result.heroes.First(Hero => Hero.id == id);
            return query.localized_name;
        }
    }
}
