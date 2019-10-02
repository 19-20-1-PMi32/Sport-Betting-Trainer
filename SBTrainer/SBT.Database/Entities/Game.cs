using System;
using System.Collections.Generic;

namespace SBT.Database.Entities
{
    public class Game
    {
        public int Id { get; set; }

        public string Team1 { get; set; }

        public string Team2 { get; set; }

        public int SportDataId { get; set; }

        public SportData SportData { get; set; }

        public ICollection<Site> Sites { get; set; }

        public ICollection<Bet> Bets { get; set; }
    }
}
