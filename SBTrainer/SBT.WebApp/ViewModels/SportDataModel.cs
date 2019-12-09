using System.Collections.Generic;

namespace SBT.WebApp.ViewModels
{
    public class SportDataModel
    {
        public string Group { get; set; }

        public string Title { get; set; }

        public IList<GameModel> GameModels { get; set; }
    }
}
