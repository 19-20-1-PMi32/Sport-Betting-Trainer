using Microsoft.Extensions.Configuration;
using System.Net;
using System.IO;

namespace SBT.Core.Requesters
{
    public class OddsApi
    {
        private static readonly string token;
        private static readonly string host;

        static OddsApi()
        {
            token = new ConfigurationBuilder()
                .AddUserSecrets<OddsApi>()
                .Build()
                ["APIs:OddsApi"];
            host = "https://api.the-odds-api.com";
        }

        public static string GetSports()
        {
            WebRequest request = WebRequest.CreateHttp($"{host}/v3/sports/?apiKey={token}");

            return GetResponse(request);
        }

        public static string GetAllSports()
        {
            WebRequest request = WebRequest.CreateHttp($"{host}/v3/sports/?apiKey={token}&all=1");

            return GetResponse(request);
        }

        public static string GetOdds(string sport, string bookmakersRegion="uk", string oddsMarket = "h2h")
        {
            WebRequest request = WebRequest.CreateHttp(
                $"{host}/v3/odds/?apiKey={token}&sport={sport}&region={bookmakersRegion}&mkt={oddsMarket}"
            );

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
