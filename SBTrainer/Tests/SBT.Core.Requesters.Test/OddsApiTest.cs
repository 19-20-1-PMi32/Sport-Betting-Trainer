using Xunit;

namespace SBT.Core.Requesters.Test
{
    public class OddsApiTest
    {
        [Fact]
        public void GetSports_ResultSuccess()
        {
            var json = OddsApi.GetSports();
            var successStr = json.Substring(2, 14);

            Assert.Equal("\"success\":true", successStr);
            Assert.True(json.Length >= 28);
        }

        [Fact]
        public void GetAllSports_ResultSuccess()
        {
            var json = OddsApi.GetAllSports();
            var successStr = json.Substring(2, 14);

            Assert.Equal("\"success\":true", successStr);
            Assert.True(json.Length >= 28);
        }

        [Fact]
        public void GetOdds_ResultSuccess()
        {
            var json = OddsApi.GetOdds("soccer_spain_la_liga");
            var successStr = json.Substring(2, 14);

            Assert.Equal("\"success\":true", successStr);
            Assert.True(json.Length >= 28);
        }

        [Fact]
        public void GetOdds_ResultEmpty()
        {
            var json = OddsApi.GetOdds("qw!&39");
            var successStr = json.Substring(2, 15);

            Assert.True(json.Length <= 28);
        }

        [Fact]
        public void GetOddsWithParams_ResultSuccess()
        {
            var json = OddsApi.GetOdds("soccer_spain_la_liga", "us", "totals");
            var successStr = json.Substring(2, 14);

            Assert.Equal("\"success\":true", successStr);
            Assert.True(json.Length >= 28);
        }

        [Fact]
        public void GetOddsWithParams_ResultEmpty()
        {
            var json = OddsApi.GetOdds("qw!&39", "us", "totals");

            Assert.True(json.Length <= 28);
        }
    }
}
