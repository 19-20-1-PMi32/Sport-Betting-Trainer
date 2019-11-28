using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace SBT.Core.Parser
{
    public static class Parser
    {
        public static DTO.SportData[] ParseSports(string jsonResponse)
        {
            JObject json = JObject.Parse(jsonResponse);
            if ((bool)json["success"])
            {
                List<DTO.SportData> result = new List<DTO.SportData>();
                foreach (var sportInfo in json["data"])
                {
                    result.Add(new DTO.SportData() {
                        IsActive = (bool)sportInfo["active"],
                        Group = (string)sportInfo["group"],
                        Details = (string)sportInfo["details"],
                        Title = (string)sportInfo["title"],
                        SportId = (string)sportInfo["key"]
                    });
                }
                return result.ToArray();
            }
            else
            {
                return new DTO.SportData[0];
            }
        }

        public static DTO.GameCoefs[] ParseOdds(string jsonResponse)
        {
            JObject json = JObject.Parse(jsonResponse);
            if ((bool)json["success"])
            {
                List<DTO.GameCoefs> result = new List<DTO.GameCoefs>();
                foreach (var data in json["data"])
                {
                    List<DTO.SiteInfo> sitesInfo = new List<DTO.SiteInfo>();
                    foreach (var site in data["sites"])
                    {
                        sitesInfo.Add(new DTO.SiteInfo() {
                            Name = (string)site["site_nice"],
                            LastUpdate = (int)site["last_update"],
                            FirstWinCoef = (float)site["odds"]["h2h"][0],
                            SecondWinCoef = (float)site["odds"]["h2h"][1],
                        });
                    }
                    result.Add(new DTO.GameCoefs() {
                        Team1 = (string)data["teams"][0],
                        Team2 = (string)data["teams"][1],
                        SitesInfo = sitesInfo
                    });
                }
                return result.ToArray();
            }
            else
            {
                return null;
            }
        }

        public static DTO.GameResult[] ParseEvents(string jsonResponse)
        {
            JObject json = JObject.Parse(jsonResponse);
            List<DTO.GameResult> result = new List<DTO.GameResult>();
            foreach (var eventInfo in json["event"])
            {
                string homeScore = (string)eventInfo["intHomeScore"];
                string awayScore = (string)eventInfo["intAwayScore"];
                string eventResult = null;
                if (!string.IsNullOrEmpty(homeScore) && !string.IsNullOrEmpty(awayScore))
                {
                    int intHomeScore = int.Parse(homeScore);
                    int intAwayScore = int.Parse(awayScore);
                    if (intHomeScore < intAwayScore)
                    {
                        eventResult = "second";
                    }
                    else if (intHomeScore > intAwayScore)
                    {
                        eventResult = "first";
                    }
                    else
                    {
                        eventResult = "draw";
                    }
                }
                result.Add(new DTO.GameResult()
                {
                    Team1 = (string)eventInfo["strHomeTeam"],
                    Team2 = (string)eventInfo["strAwayTeam"],
                    Result = eventResult
                });
            }
            return result.ToArray();
        }
    }
}
