using System;

namespace SBT.Database.Entities
{
    public class Bet
    {
        public int Id { get; set; }

        public float Coefficient { get; set; }

        public float Money { get; set; }

        public int GameId { get; set; }

        public string AccountEmail { get; set; }

        public Game Game { get; set; }

        public Account Account { get; set; }
    }
}
