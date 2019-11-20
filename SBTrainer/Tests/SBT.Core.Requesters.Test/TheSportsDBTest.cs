using Xunit;

namespace SBT.Core.Requesters.Test
{
    public class TheSportsDBTest
    {
        [Fact]
        public void GetSports_ResultNotNull()
        {
            var json = TheSportsDB.GetTeamByName("Arsenal");

            Assert.True(json.Length >= 16);
        }

        [Fact]
        public void GetSports_ResultNull()
        {
            var json = TheSportsDB.GetTeamByName("Ar62$");
            var resultStr = json.Substring(9, 4);

            Assert.Equal("null", resultStr);
            Assert.True(json.Length <= 16);
        }

        [Fact]
        public void GetEvent_ResultNotNull()
        {
            var json = TheSportsDB.GetEvent("Arsenal_vs_Chelsea");

            Assert.True(json.Length >= 16);
        }

        [Fact]
        public void GetEvent_ResultNull()
        {
            var json = TheSportsDB.GetEvent("Ar62$_vs_Csk47");
            var resultStr = json.Substring(9, 4);

            Assert.Equal("null", resultStr);
            Assert.True(json.Length <= 16);
        }

        [Fact]
        public void GetEventsByDate_ResultNotNull()
        {
            var json = TheSportsDB.GetEventsByDate("2014-10-10");

            Assert.True(json.Length >= 17);
        }

        [Fact]
        public void GetEventsByDate_ResultNull()
        {
            var json1 = TheSportsDB.GetEventsByDate("2050-10-10");
            var json2 = TheSportsDB.GetEventsByDate("qws737");
            var resultStr = json1.Substring(10, 4);

            Assert.Equal("null", resultStr);
            Assert.True(json1.Length <= 17);

            Assert.Equal(0, json2.Length);
        }
    }
}
