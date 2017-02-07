using System.Collections.Generic;

namespace DotaApiManage
{
    public class Player
    {
        public string account_id { get; set; }
        public int player_slot { get; set; }
        public int hero_id { get; set; }
    }

    public class Match
    {
        public string match_id { get; set; }
        public string match_seq_num { get; set; }
        public int start_time { get; set; }
        public int lobby_type { get; set; }
        public int radiant_team_id { get; set; }
        public int dire_team_id { get; set; }
        public List<Player> players { get; set; }
    }

    public class Result
    {
        public int status { get; set; }
        public int num_results { get; set; }
        public int total_results { get; set; }
        public int results_remaining { get; set; }
        public List<Match> matches { get; set; }
    }

    public class BaseResultSet
    {
        public Result result { get; set; }
    }
}
