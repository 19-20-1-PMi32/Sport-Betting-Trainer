using System;
using System.Collections.Generic;

namespace SBT.Database.Entities
{
    public class Site
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int LastUpdate { get; set; }

        public float FirstWin { get; set; }

        public float SecondWin { get; set; }

        public float? Draw { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }
    }
}
