using System.Net;
using System.IO;

namespace SBT.Core.Requesters
{
    public class TheSportsDB
    {
        private static readonly string host;

        static TheSportsDB()
        {
            host = "https://www.thesportsdb.com/api/v1/json/1";
        }

        public static string GetTeamByName(string teamName)
        {
            WebRequest request = WebRequest.CreateHttp($"{host}/searchteams.php?t={teamName}");

            return GetResponse(request);
        }

        public static string GetEvent(string eventName)
        {
            WebRequest request = WebRequest.CreateHttp($"{host}/searchevents.php?e={eventName}");

            return GetResponse(request);
        }

        public static string GetEventsByDate(string date)
        {
            WebRequest request = WebRequest.CreateHttp($"{host}/eventsday.php?d={date}");

            return GetResponse(request);
        }

        private static string GetResponse(WebRequest request)
        {
            WebResponse response = request.GetResponse();
            string json;

            if (((HttpWebResponse)response).StatusCode == HttpStatusCode.OK)
            {
                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream);
                    json = reader.ReadToEnd();
                }
            }
            else
            {
                json = "";
            }

            return json;
        }
    }
}
