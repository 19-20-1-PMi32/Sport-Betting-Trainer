using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace SBT.Core.Parser
{
    class Parser
    {
        public object[] ParseSports(string jsonResponse)
        {
            JObject json = JObject.Parse(jsonResponse);
            if ((bool)json["success"])
            {
                List<object> result = new List<object>();
                foreach (var sportInfo in json["data"])
                {
                    result.Add(new {
                        isActive = (string)sportInfo["active"],
                        group = (string)sportInfo["group"],
                        details = (string)sportInfo["details"],
                        title = (string)sportInfo["title"],
                        sportId = (string)sportInfo["key"]
                    });
                }
                return result.ToArray();
            }
            else
            {
                return null;
            }
        }

        public DTO.GameCoefs[] ParseOdds(string jsonResponse)
        {
            JObject json = JObject.Parse(jsonResponse);
            if ((bool)json["success"])
            {
                List<DTO.GameCoefs> result = new List<DTO.GameCoefs>();
                foreach (var data in json["data"])
                {
                    List<object> sitesInfo = new List<object>();
                    foreach (var site in data["sites"])
                    {
                        sitesInfo.Add(new {
                            name = (string)site["site_nice"],
                            lastUpdate = (int)site["last_update"],
                            firstWinCoef = (float)site["odds"]["h2h"][0],
                            secondWinCoef = (float)site["odds"]["h2h"][1],
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

        public DTO.GameResult[] ParseEvents(string jsonResponse)
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
