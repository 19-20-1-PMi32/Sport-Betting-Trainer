namespace SBT.WebApp.ViewModels
{
    public class MakeBetPartialModel
    {
        public string SportName { get; set; }

        public string Team1 { get; set; }

        public float Team1Coef { get; set; }

        public float DrawCoef { get; set; }

        public string Team2 { get; set; }

        public float Team2Coef { get; set; }

        public float Money { get; set; }

        public string Type { get; set; }

        public int GameId { get; set; }
    }
}
