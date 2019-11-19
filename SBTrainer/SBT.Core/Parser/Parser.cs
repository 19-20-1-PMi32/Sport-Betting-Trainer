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

        public object[] ParseOdds(string jsonResponse)
        {
            return new { };
        }
    }
}
