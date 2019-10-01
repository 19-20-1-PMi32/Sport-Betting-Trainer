using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SBT.Database.Entities
{
    public class Game
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Team1 { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Team2 { get; set; }

        public int SportDataId { get; set; }

        public SportData SportData { get; set; }

        public ICollection<Site> Sites { get; set; }

        public ICollection<Bet> Bets { get; set; }
    }
}
